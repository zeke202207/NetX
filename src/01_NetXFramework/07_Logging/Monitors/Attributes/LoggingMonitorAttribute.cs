using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using NetX.Common;
using NetX.Tenants;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetX.Logging.Monitors;

/// <summary>
/// 日志监听器
/// </summary>
/// <remarks>主要用于将请求的信息打印出来</remarks>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public sealed class LoggingMonitorAttribute : Attribute, IAsyncActionFilter, IActionFilter, IOrderedFilter
{
    /// <summary>
    /// 模板正则表达式对象
    /// </summary>
    private readonly Lazy<Regex> _lazyRegex = new(() => new(@"^##(?<prop>.*)?##[:：]?\s*(?<content>.*)"));

    /// <summary>
    /// 过滤器排序
    /// </summary>
    private const int FilterOrder = -2000;

    /// <summary>
    /// 排序属性
    /// </summary>
    public int Order => FilterOrder;

    /// <summary>
    /// 日志 LogName
    /// </summary>
    /// <remarks>方便对日志进行过滤写入不同的存储介质中</remarks>
    internal const string LOG_CATEGORY_NAME = "System.Logging.LoggingMonitor";

    /// <summary>
    /// 构造函数
    /// </summary>
    public LoggingMonitorAttribute()
        : this(new LoggingMonitorSettings())
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="settings"></param>
    internal LoggingMonitorAttribute(LoggingMonitorSettings settings)
    {
        Settings = settings;
    }

    /// <summary>
    /// 日志标题
    /// </summary>
    public string Title { get; set; } = "Logging Monitor";

    /// <summary>
    /// 是否记录返回值
    /// </summary>
    /// <remarks>bool 类型，默认输出</remarks>
    public object WithReturnValue { get; set; } = null;

    /// <summary>
    /// 设置返回值阈值
    /// </summary>
    /// <remarks>配置返回值字符串阈值，超过这个阈值将截断，默认全量输出</remarks>
    public object ReturnValueThreshold { get; set; } = null;

    /// <summary>
    /// 配置 Json 输出行为
    /// </summary>
    public object JsonBehavior { get; set; } = null;

    /// <summary>
    /// 配置信息
    /// </summary>
    private LoggingMonitorSettings Settings { get; set; }

    /// <summary>
    /// 监视 Action 执行
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 获取控制器/操作描述器
        var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        // 获取请求方法
        var actionMethod = controllerActionDescriptor.MethodInfo;
        // 如果贴了 [SuppressMonitor] 特性则跳过
        if (actionMethod.IsDefined(typeof(SuppressMonitorAttribute), true))
        {
            _ = await next();
            return;
        }
        // 获取方法完整名称
        var methodFullName = controllerActionDescriptor.ControllerTypeInfo.FullName + "." + actionMethod.Name;
        // 创建 json 写入器
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, Settings.JsonWriterOptions);
        writer.WriteStartObject();
        // 创建日志上下文
        var logContext = new LogContext();
        // 获取路由表信息
        var routeData = context.RouteData;
        var controllerName = routeData.Values["controller"];
        var actionName = routeData.Values["action"];
        var areaName = routeData.DataTokens["area"];
        writer.WriteString(nameof(controllerName), controllerName?.ToString());
        writer.WriteString("controllerTypeName", controllerActionDescriptor.ControllerTypeInfo.Name);
        writer.WriteString(nameof(actionName), actionName?.ToString());
        writer.WriteString("actionTypeName", actionMethod.Name);
        writer.WriteString("areaName", areaName?.ToString());
        // 调用呈现链名称
        var displayName = controllerActionDescriptor.DisplayName;
        writer.WriteString(nameof(displayName), displayName);
        // 获取 HttpContext 和 HttpRequest 对象
        var httpContext = context.HttpContext;
        var httpRequest = httpContext.Request;
        // 获取服务端 IPv4 地址
        var localIPv4 = httpContext.GetLocalIpAddressToIPv4();
        writer.WriteString(nameof(localIPv4), localIPv4);
        // 获取客户端 IPv4 地址
        var remoteIPv4 = httpContext.GetRemoteIpAddressToIPv4();
        writer.WriteString(nameof(remoteIPv4), remoteIPv4);
        // 获取请求方式
        var httpMethod = httpContext.Request.Method;
        writer.WriteString(nameof(httpMethod), httpMethod);
        // 获取请求的 Url 地址
        var requestUrl = Uri.UnescapeDataString(httpRequest.GetRequestUrlAddress());
        writer.WriteString(nameof(requestUrl), requestUrl);
        // 获取来源 Url 地址
        var refererUrl = Uri.UnescapeDataString(httpRequest.GetRefererUrlAddress());
        writer.WriteString(nameof(refererUrl), refererUrl);
        // 服务器环境
        var environment = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>().EnvironmentName;
        writer.WriteString(nameof(environment), environment);
        // 客户端浏览器信息
        var userAgent = httpRequest.Headers["User-Agent"];
        writer.WriteString(nameof(userAgent), userAgent);
        // 获取方法参数
        var parameterValues = context.ActionArguments;
        // 获取授权用户
        var user = httpContext.User;
        // token 信息
        var authorization = httpRequest.Headers["Authorization"].ToString();
        writer.WriteString("requestHeaderAuthorization", authorization);
        // 计算接口执行时间
        var timeOperation = Stopwatch.StartNew();
        var resultContext = await next();
        timeOperation.Stop();
        writer.WriteNumber("timeOperationElapsedMilliseconds", timeOperation.ElapsedMilliseconds);
        // 获取异常对象情况
        var exception = resultContext.Exception;
        var monitorItems = new List<string>()
        {
            $"##控制器名称## {controllerActionDescriptor.ControllerTypeInfo.Name}"
            , $"##操作名称## {actionMethod.Name}"
            , $"##路由信息## [area]: {areaName}; [controller]: {controllerName}; [action]: {actionName}"
            , $"##请求方式## {httpMethod}"
            , $"##请求地址## {requestUrl}"
            , $"##来源地址## {refererUrl}"
            , $"##浏览器标识## {userAgent}"
            , $"##客户端 IP 地址## {remoteIPv4}"
            , $"##服务端 IP 地址## {localIPv4}"
            , $"##服务端运行环境## {environment}"
            , $"##执行耗时## {timeOperation.ElapsedMilliseconds}ms"
        };
        // 添加 JWT 授权信息日志模板
        monitorItems.AddRange(GenerateAuthorizationTemplate(writer, user, authorization));
        // 添加请求参数信息日志模板
        monitorItems.AddRange(GenerateParameterTemplate(writer, parameterValues, actionMethod, httpRequest.Headers["Content-Type"]));
        // 判断是否启用返回值打印
        if (Settings.WithReturnValue)
        {
            // 添加返回值信息日志模板
            monitorItems.AddRange(GenerateReturnInfomationTemplate(writer, resultContext, actionMethod));
        }
        // 添加异常信息日志模板
        monitorItems.AddRange(GenerateExcetpionInfomationTemplate(writer, exception));
        // 生成最终模板
        var monitorMessage = Wrapper(Title, displayName, monitorItems.ToArray());
        // 创建日志记录器
        var logger = httpContext.RequestServices.GetRequiredService<ILoggerFactory>()
            .CreateLogger(LOG_CATEGORY_NAME);
        // 调用外部配置
        LoggingMonitorSettings.Configure?.Invoke(logger, logContext, resultContext);
        writer.WriteEndObject();
        writer.Flush();
        // 获取 json 字符串
        var jsonString = Encoding.UTF8.GetString(stream.ToArray());
        logContext.Set(LoggingConst.C_LOGGING_MONITOR_KEY, jsonString);
        logContext.Set(LoggingConst.C_LOGGING_TENANTCONTEXT_KEY, TenantContext.CurrentTenant);
        logContext.Set(LoggingConst.C_LOGGING_AUDIT, actionMethod.IsDefined(typeof(AuditAttribute), true));
        // 设置日志上下文
        logger.ScopeContext(logContext);
        // 获取最终写入日志消息格式
        var finalMessage = Settings.JsonBehavior == NetX.Logging.Monitors.JsonBehavior.OnlyJson ? jsonString : monitorMessage;
        // 写入日志，如果没有异常使用 LogInformation，否则使用 LogError
        if (exception == null) 
            logger.LogInformation(finalMessage);
        else
            logger.LogError(exception, finalMessage);
    }

    /// <summary>
    /// 生成 JWT 授权信息日志模板
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="claimsPrincipal"></param>
    /// <param name="authorization"></param>
    /// <returns></returns>
    private List<string> GenerateAuthorizationTemplate(Utf8JsonWriter writer, ClaimsPrincipal claimsPrincipal, StringValues authorization)
    {
        var templates = new List<string>();
        if (!claimsPrincipal.Claims.Any()) 
            return templates;
        templates.AddRange(new[]
        {
            $"━━━━━━━━━━━━━━━  授权信息 ━━━━━━━━━━━━━━━"
            , $"##JWT Token## {authorization}"
            , $""
        });
        // 遍历身份信息
        writer.WritePropertyName("authorizationClaims");
        writer.WriteStartArray();
        foreach (var claim in claimsPrincipal.Claims)
        {
            var valueType = claim.ValueType.Replace("http://www.w3.org/2001/XMLSchema#", "");
            writer.WriteStartObject();
            templates.Add($"##{claim.Type} ({valueType})## {claim.Value}");
            writer.WriteString("type", claim.Type);
            writer.WriteString("valueType", valueType);
            writer.WriteString("value", claim.Value);
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
        return templates;
    }

    /// <summary>
    /// 生成请求参数信息日志模板
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="parameterValues"></param>
    /// <param name="method"></param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    private List<string> GenerateParameterTemplate(Utf8JsonWriter writer, IDictionary<string, object> parameterValues, MethodInfo method, StringValues contentType)
    {
        var templates = new List<string>();
        writer.WritePropertyName("parameters");
        if (parameterValues.Count == 0)
        {
            writer.WriteStartArray();
            writer.WriteEndArray();
            return templates;
        }
        templates.AddRange(new[]
        {
            $"━━━━━━━━━━━━━━━  参数列表 ━━━━━━━━━━━━━━━"
            , $"##Content-Type## {contentType}"
            , $""
        });
        var parameters = method.GetParameters();
        writer.WriteStartArray();
        foreach (var parameter in parameters)
        {
            var name = parameter.Name;
            _ = parameterValues.TryGetValue(name, out var value);
            var parameterType = parameter.ParameterType;
            writer.WriteStartObject();
            writer.WriteString("name", name);
            writer.WriteString("type", parameterType.FullName);
            object rawValue = default;
            // 文件类型参数
            if (value is IFormFile || value is List<IFormFile>)
            {
                writer.WritePropertyName("value");
                // 单文件
                if (value is IFormFile formFile)
                {
                    var fileSize = Math.Round(formFile.Length / 1024D);
                    templates.Add($"##{name} ({parameterType.Name})## [name]: {formFile.FileName}; [size]: {fileSize}KB; [content-type]: {formFile.ContentType}");

                    writer.WriteStartObject();
                    writer.WriteString(name, formFile.Name);
                    writer.WriteString("fileName", formFile.FileName);
                    writer.WriteNumber("length", formFile.Length);
                    writer.WriteString("contentType", formFile.ContentType);
                    writer.WriteEndObject();

                    goto writeEndObject;
                }
                // 多文件
                else if (value is List<IFormFile> formFiles)
                {
                    writer.WriteStartArray();
                    for (var i = 0; i < formFiles.Count; i++)
                    {
                        var file = formFiles[i];
                        var size = Math.Round(file.Length / 1024D);
                        templates.Add($"##{name}[{i}] ({nameof(IFormFile)})## [name]: {file.FileName}; [size]: {size}KB; [content-type]: {file.ContentType}");

                        writer.WriteStartObject();
                        writer.WriteString(name, file.Name);
                        writer.WriteString("fileName", file.FileName);
                        writer.WriteNumber("length", file.Length);
                        writer.WriteString("contentType", file.ContentType);
                        writer.WriteEndObject();
                    }
                    writer.WriteEndArray();
                    goto writeEndObject;
                }
            }
            // 处理 byte[] 参数类型
            else if (value is byte[] byteArray)
            {
                writer.WritePropertyName("value");
                templates.Add($"##{name} ({parameterType.Name})## [Length]: {byteArray.Length}");
                writer.WriteStartObject();
                writer.WriteNumber("length", byteArray.Length);
                writer.WriteEndObject();
                goto writeEndObject;
            }
            // 处理基元类型，字符串类型和空值
            else if (parameterType.IsPrimitive || value is string || value == null)
            {
                writer.WritePropertyName("value");
                rawValue = value;
                if (value == null) 
                    writer.WriteNullValue();
                else if (value is string str) 
                    writer.WriteStringValue(str);
                else if (double.TryParse(value.ToString(), out var r)) 
                    writer.WriteNumberValue(r);
                else 
                    writer.WriteStringValue(value.ToString());
            }
            // 其他类型统一进行序列化
            else
            {
                writer.WritePropertyName("value");
                rawValue = TrySerializeObject(value, out var succeed);
                if (succeed) 
                    writer.WriteRawValue(rawValue?.ToString());
                else 
                    writer.WriteNullValue();
            }
            templates.Add($"##{name} ({parameterType.Name})## {rawValue}");
        writeEndObject: writer.WriteEndObject();
        }
        writer.WriteEndArray();
        return templates;
    }

    /// <summary>
    /// 生成返回值信息日志模板
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="resultContext"></param>
    /// <param name="method"></param>
    /// <param name="monitorMethod"></param>
    /// <returns></returns>
    private List<string> GenerateReturnInfomationTemplate(Utf8JsonWriter writer, ActionExecutedContext resultContext, MethodInfo method)
    {
        var templates = new List<string>();
        object returnValue = resultContext.Result;
        var succeed = true;
        // 获取最终呈现值（字符串类型）
        var displayValue = method.ReturnType == typeof(void)
            ? string.Empty
            : TrySerializeObject(returnValue, out succeed);
        var originValue = displayValue;
        // 获取返回值阈值
        var threshold = Settings.ReturnValueThreshold;
        if (threshold > 0)
            displayValue = displayValue[..(displayValue.Length > threshold ? threshold : displayValue.Length)];
        templates.AddRange(new[]
        {
            $"━━━━━━━━━━━━━━━  返回信息 ━━━━━━━━━━━━━━━"
            , $"##原始类型## {method.ReturnType.FullName}"
            , $"##最终类型## {returnValue?.GetType()?.FullName}"
            , $"##最终返回值## {displayValue}"
        });
        writer.WritePropertyName("returnInformation");
        writer.WriteStartObject();
        writer.WriteString("type", returnValue?.GetType()?.FullName);
        writer.WriteString("actType", method.ReturnType.FullName);
        writer.WritePropertyName("value");
        if (succeed && method.ReturnType != typeof(void))
        {
            // 解决返回值被截断后 json 验证失败异常问题
            if (threshold > 0 && originValue != displayValue) writer.WriteStringValue(displayValue);
            else writer.WriteRawValue(displayValue);
        }
        else writer.WriteNullValue();
        writer.WriteEndObject();
        return templates;
    }

    /// <summary>
    /// 生成异常信息日志模板
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="exception"></param>
    /// <param name="isValidationException">是否是验证异常</param>
    /// <returns></returns>
    private List<string> GenerateExcetpionInfomationTemplate(Utf8JsonWriter writer, Exception exception)
    {
        var templates = new List<string>();
        if (exception == null)
        {
            writer.WritePropertyName("exception");
            writer.WriteNullValue();
            writer.WritePropertyName("validation");
            writer.WriteNullValue();
            return templates;
        }
        // 处理不是验证异常情况
        templates.AddRange(new[]
        {
                $"━━━━━━━━━━━━━━━  异常信息 ━━━━━━━━━━━━━━━"
                , $"##类型## {exception.GetType().FullName}"
                , $"##消息## {exception.Message}"
                , $"##错误堆栈## {exception.StackTrace}"
         });
        writer.WritePropertyName("exception");
        writer.WriteStartObject();
        writer.WriteString("type", exception.GetType().FullName);
        writer.WriteString("message", exception.Message);
        writer.WriteString("stackTrace", exception.StackTrace.ToString());
        writer.WriteEndObject();
        writer.WritePropertyName("validation");
        writer.WriteNullValue();
        return templates;
    }

    /// <summary>
    /// 序列化默认配置
    /// </summary>
    private static readonly JsonSerializerSettings _serializerSettings = new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    };

    /// <summary>
    /// 序列化对象
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="succeed"></param>
    /// <returns></returns>
    private static string TrySerializeObject(object obj, out bool succeed)
    {
        try
        {
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(obj, _serializerSettings);
            succeed = true;
            return result;
        }
        catch
        {
            succeed = true;
            return "<Error Serialize>";
        }
    }

    /// <summary>
    /// 生成规范日志模板
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="description">描述</param>
    /// <param name="items">列表项，如果以 ##xxx## 开头，自动生成 xxx: 属性</param>
    /// <returns><see cref="string"/></returns>
    public string Wrapper(string title, string description, params string[] items)
    {
        // 处理不同编码问题
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"┏━━━━━━━━━━━  {title} ━━━━━━━━━━━").AppendLine();
        // 添加描述
        if (!string.IsNullOrWhiteSpace(description))
            stringBuilder.Append($"┣ {description}").AppendLine().Append("┣ ").AppendLine();
        if (items != null && items.Length > 0)
        {
            var propMaxLength = items.Where(u => _lazyRegex.Value.IsMatch(u))
                .DefaultIfEmpty(string.Empty)
                .Max(u => _lazyRegex.Value.Match(u).Groups["prop"].Value.Length);
            propMaxLength += (propMaxLength >= 5 ? 10 : 5);
            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                if (_lazyRegex.Value.IsMatch(item))
                {
                    var match = _lazyRegex.Value.Match(item);
                    var prop = match.Groups["prop"].Value;
                    var content = match.Groups["content"].Value;
                    var propTitle = $"{prop}：";
                    stringBuilder.Append($"┣ {PadRight(propTitle, propMaxLength)}{content}").AppendLine();
                }
                else
                    stringBuilder.Append($"┣ {item}").AppendLine();
            }
        }
        stringBuilder.Append($"┗━━━━━━━━━━━  {title} ━━━━━━━━━━━");
        return stringBuilder.ToString();
    }

    /// <summary>
    /// 等宽文字对齐
    /// </summary>
    /// <param name="str"></param>
    /// <param name="totalByteCount"></param>
    /// <returns></returns>
    private string PadRight(string str, int totalByteCount)
    {
        var coding = Encoding.GetEncoding("gbk");
        var dcount = 0;
        foreach (var ch in str.ToCharArray())
        {
            if (coding.GetByteCount(ch.ToString()) == 2)
                dcount++;
        }
        var w = str.PadRight(totalByteCount - dcount);
        return w;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }
}
