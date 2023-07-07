using Microsoft.AspNetCore.Mvc;
using NetX.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Caching.Controllers
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
