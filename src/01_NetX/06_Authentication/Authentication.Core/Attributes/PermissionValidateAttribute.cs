using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace NetX.Authentication.Core;

/// <summary>
/// 权限验证特性
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class PermissionValidateAttribute : AuthorizeAttribute, IAuthorizationFilter, IAsyncAuthorizationFilter
{
    /// <summary>
    /// 鉴权验证
    /// </summary>
    /// <param name="context"></param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        //排除匿名访问
        if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(AllowAnonymousAttribute)))
            return;
        //var loginInfo = context.HttpContext.RequestServices.GetService<LoginInfo>();
        //if (null == loginInfo || string.IsNullOrEmpty(loginInfo.AccountId))
        //{
        //    context.Result = new ChallengeResult();
        //    return;
        //}
        //排除通用接口
        if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(NoPermissionAttribute)))
            return;
        var handle = context.HttpContext.RequestServices.GetService<IPermissionValidateHandler>();
        if (null == handle || !handle.Validate(context.HttpContext, context.ActionDescriptor.RouteValues))
            context.Result = new ForbidResult();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        OnAuthorization(context);
        return Task.CompletedTask;
    }
}
