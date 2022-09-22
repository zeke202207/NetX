using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetX.App;

/// <summary>
/// api接口提供基类
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class ApiBaseController : BaseController
{
}
