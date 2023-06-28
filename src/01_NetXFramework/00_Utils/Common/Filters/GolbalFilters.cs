using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;

namespace NetX.Common;

#region 00_Authorization Filters

#endregion

#region 01_Resource Filters

/// <summary>
/// 资源过滤器
/// </summary>
public class ResourceFilter : BaseFilter, IResourceFilter, IAsyncResourceFilter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void OnResourceExecuting(ResourceExecutingContext context)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void OnResourceExecuted(ResourceExecutedContext context)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        OnResourceExecuting(context);
        OnResourceExecuted(await next());
    }
}

#endregion

#region 02_Action Filters

/// <summary>
/// Action过滤器
/// </summary>
public class ActionsFilter : BaseFilter, IActionFilter, IAsyncActionFilter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        OnActionExecuting(context);
        OnActionExecuted(await next());

        sw.Stop();
        //增加计时器
        context.HttpContext.Response.Headers.Add("duration", sw.Elapsed.TotalMilliseconds.ToString());
    }
}

#endregion

#region 03_Exception Filters

/// <summary>
/// 异常过滤器
/// </summary>
public class ExceptionFilter : BaseFilter, IExceptionFilter, IAsyncExceptionFilter
{
    private readonly ILogger<ExceptionFilter> logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void OnException(ExceptionContext context)
    {
        if (context.ExceptionHandled)
            return;
        context.ExceptionHandled = true;
        int Code = 500;
        if (context.Exception.GetType().BaseType == typeof(ExceptionBase))
            Code = ((ExceptionBase)context.Exception).StatusCode;
        context.Result = new JsonResult(context.Exception.ToString())
        {
            StatusCode = Code
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task OnExceptionAsync(ExceptionContext context)
    {
        OnException(context);
        return Task.CompletedTask;
    }
}

#endregion

#region 04_Result Filters

/// <summary>
/// 结果过滤器
/// </summary>
public class ResultFilter : BaseFilter, IResultFilter, IAsyncResultFilter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void OnResultExecuting(ResultExecutingContext context)
    {
        var objr = context.Result as ObjectResult;
        if (objr == null)
            return;
        context.Result = new JsonResult(objr.Value, new JsonSerializerSettings()
        {
            //日期格式
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
            //空值处理
            NullValueHandling = NullValueHandling.Ignore,
            //ContractResolver = new CamelCasePropertyNamesContractResolver(),
            //缩进设置
            Formatting = Formatting.Indented,
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void OnResultExecuted(ResultExecutedContext context) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        OnResultExecuting(context);
        OnResultExecuted(await next());
    }
}

#endregion
