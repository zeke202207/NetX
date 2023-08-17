using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;


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

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        OnAuthorization(context);
        //排除匿名和不需要授权的接口访问
        if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(AllowAnonymousAttribute) || m.GetType() == typeof(NoPermissionAttribute)))
            return;
        var handle = context.HttpContext.RequestServices.GetService<IPermissionValidateHandler>();
        if (null == handle || !await handle.Validate(context.HttpContext, context.ActionDescriptor.RouteValues))
            context.Result = new ForbidResult();
    }
}
