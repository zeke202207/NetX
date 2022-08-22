using Microsoft.AspNetCore.Mvc;

namespace NetX.App;

//[TypeFilter(typeof(ResourceFilter))]
//[TypeFilter(typeof(ActionsFilter))]
//[TypeFilter(typeof(ExceptionFilter))]
//[TypeFilter(typeof(ResultFilter))]
//[TypeFilter(typeof(ErrorCodeExceptionFilter), Order = 999)]
public class BaseController : ControllerBase
{
}
