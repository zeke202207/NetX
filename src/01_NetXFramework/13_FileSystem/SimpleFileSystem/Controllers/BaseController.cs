using Microsoft.AspNetCore.Mvc;

namespace NetX.SimpleFileSystem
{
    /// <summary>
    /// api接口基类对象
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {

    }
}
