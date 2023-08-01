using Microsoft.AspNetCore.Mvc;
using NetX.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.ModuleManager.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class ModuleManagerBaseController : ApiPermissionController
    {
    }
}
