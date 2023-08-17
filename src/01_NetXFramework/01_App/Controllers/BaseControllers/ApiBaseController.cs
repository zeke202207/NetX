using Microsoft.AspNetCore.Mvc;

namespace NetX.App;

/// <summary>
/// api接口提供基类
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class ApiBaseController : BaseController
{
}
