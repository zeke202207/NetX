using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetX.App;

[Route("api/[controller]/[action]")]
[ApiController]
public class ApiBaseController : BaseController
{
}
