using Microsoft.AspNetCore.Mvc;
using NetX.App;

namespace NetX.ModuleManager.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class ModuleManagerBaseController : ApiPermissionController
    {
    }
}
