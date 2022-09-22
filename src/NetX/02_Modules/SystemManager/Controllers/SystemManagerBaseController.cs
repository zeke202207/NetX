using Microsoft.AspNetCore.Mvc;
using NetX.App;
using NetX.Common.Models;
using NetX.SystemManager.Models;

namespace NetX.SystemManager.Controllers;

/// <summary>
/// api接口基类对象
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public abstract class SystemManagerBaseController : ApiPermissionController
{
    /// <summary>
    /// 返回成功结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    protected JsonResult Success<T>(T result)
    {
        return new JsonResult(new ResultModel<T>(ResultEnum.SUCCESS) { Result = result });
    }

    /// <summary>
    /// 返回失败结果
    /// </summary>
    /// <param name="resultEnum"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    protected JsonResult Error(ResultEnum resultEnum, string msg)
    {
        return new JsonResult(new ResultModel<LoginResult>(resultEnum) { Message = msg });
    }
}
