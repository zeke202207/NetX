using Microsoft.AspNetCore.Mvc;
using NetX.App;

namespace NetX.TaskScheduling.Controllers
{
    /// <summary>
    /// api接口基类对象
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ApiPermissionController
    {

    }
}
