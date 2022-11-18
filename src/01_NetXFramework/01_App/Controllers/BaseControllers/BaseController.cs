using Microsoft.AspNetCore.Mvc;
using NetX.Common;

namespace NetX.App;

/// <summary>
/// 控制器基类
/// </summary>
//[TypeFilter(typeof(ResourceFilter))]
[TypeFilter(typeof(ActionsFilter))]
[TypeFilter(typeof(ExceptionFilter))]
[TypeFilter(typeof(ResultFilter))]
//[TypeFilter(typeof(ErrorCodeExceptionFilter), Order = 999)]
public class BaseController : ControllerBase
{
}
