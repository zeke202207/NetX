using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NetX.MemoryQueue;
using NetX.Tenants;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;

namespace NetX.AuditLog
{
    /// <summary>
    /// audito log filter
    /// </summary>
    public class AuditLogActionFilter : IAsyncActionFilter
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        private readonly ILogger<AuditLogActionFilter> _logger;
        private readonly AuditOption _auditOption;
        private readonly IClientProvider _clientProvider;
        private readonly IPublisher _publisher;

        /// <summary>
        /// the instance of <see cref="AuditLogActionFilter"/>
        /// </summary>
        public AuditLogActionFilter(ILogger<AuditLogActionFilter> logger, AuditOption auditOption, IClientProvider clientProvider, IPublisher publisher)
        {
            _logger = logger;
            _auditOption = auditOption;
            _clientProvider = clientProvider;
            _publisher = publisher;
        }

        /// <summary>
        ///  handle every action
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 判断是否写日志
            if (!ShouldSaveAudit(context) || !_auditOption.Enabled)
            {
                await next();
                return;
            }
            ControllerActionDescriptor controllerActionDesc = context.ActionDescriptor as ControllerActionDescriptor;
            //接口Type
            var type = controllerActionDesc.ControllerTypeInfo.AsType();
            //方法信息
            var method = controllerActionDesc.MethodInfo;
            //方法参数
            var arguments = context.ActionArguments;
            //开始计时
            var stopwatch = Stopwatch.StartNew();
            var auditInfo = new AuditInfo
            {
                UserId = TenantContext.CurrentTenant?.Principal?.UserId,
                ServiceName = type != null ? type.FullName : "",
                MethodName = method.Name,
                ////请求参数转Json
                Parameters = JsonConvert.SerializeObject(arguments),
                ExecutionTime = DateTime.Now,
                BrowserInfo = _clientProvider.BrowserInfo,
                ClientIpAddress = _clientProvider.ClientIpAddress,
                //ClientName = _clientProvider.ClientName,
                Id = Guid.NewGuid().ToString()
            };
            ActionExecutedContext result = null;
            try
            {
                result = await next();
                if (result.Exception != null && !result.ExceptionHandled)
                    auditInfo.Exception = result.Exception;
            }
            catch (Exception ex)
            {
                auditInfo.Exception = ex;
                _logger.LogError("审计过滤器异常", ex);
                throw;
            }
            finally
            {
                stopwatch.Stop();
                auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
                if (result != null)
                {
                    switch (result.Result)
                    {
                        case ObjectResult objectResult:
                            auditInfo.ReturnValue = JsonConvert.SerializeObject(objectResult.Value);
                            break;
                        case JsonResult jsonResult:
                            auditInfo.ReturnValue = JsonConvert.SerializeObject(jsonResult.Value);
                            break;
                        case ContentResult contentResult:
                            auditInfo.ReturnValue = contentResult.Content;
                            break;
                    }
                }
                //保存审计日志
                await _publisher.Publish(NetxConstEnum.C_QUEUE_AUDITLOG_HANDLE, new AuditLogConsumerModel(TenantContext.CurrentTenant.Principal.Tenant.TenantId, auditInfo));
            }
        }

        /// <summary>
        /// 是否需要记录审计
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool ShouldSaveAudit(ActionExecutingContext context)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor))
                return false;
            var methodInfo = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo;
            if (methodInfo == null)
                return false;
            if (!methodInfo.IsPublic)
                return false;
            if (methodInfo.GetCustomAttribute<AuditedAttribute>() != null)
                return true;
            if (methodInfo.GetCustomAttribute<DisableAuditingAttribute>() != null)
                return false;
            var classType = methodInfo.DeclaringType;
            if (classType != null)
            {
                if (classType.GetTypeInfo().GetCustomAttribute<AuditedAttribute>() != null)
                    return true;
                if (classType.GetTypeInfo().GetCustomAttribute<DisableAuditingAttribute>() != null)
                    return false;
            }
            return false;
        }
    }
}
