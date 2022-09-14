using Microsoft.AspNetCore.Mvc;
using NetX.App;
using NetX.Common.Models;
using NetX.Swagger;
using NetX.SystemManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class SystemManagerBaseController : ApiPermissionController
    {
        protected JsonResult Success<T>(T result)
        {
            return new JsonResult(new ResultModel<T>(ResultEnum.SUCCESS) { Result = result });
        }

        protected JsonResult Error(ResultEnum resultEnum,string msg)
        {
            return new JsonResult(new ResultModel<LoginResult>(resultEnum) { Message = msg });
        }
    }
}
