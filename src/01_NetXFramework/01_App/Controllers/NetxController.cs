using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.DatabaseSetup;
using NetX.Swagger;
using NetX.Tenants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.App;

/// <summary>
/// 框架服务接口
/// </summary>
[ApiControllerDescription(SwaggerConst.C_NOGROUP_NAME, Description = "NetX框架提供的服务接口->数据库迁移管理")]
public class NetxController : ApiBaseController
{
    private readonly IMigrationService _migrationService;

    /// <summary>
    /// 框架服务接口
    /// </summary>
    /// <param name="migrationService"></param>
    public NetxController(IMigrationService migrationService)
    {
        this._migrationService = migrationService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("创建（升级）数据库或者数据表")]
    [NoPermission]
    [HttpPost]
    public async Task<bool> MigrateUp()
    {
        return await _migrationService.MigrateUp();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("删除数据表或数据，请谨慎操作(生产环境中尤为慎重，避免造成数据丢失)")]
    [NoPermission]
    [HttpPost]
    public async Task<bool> MigrateDown(long version)
    {
        return await _migrationService.MigrateDown(version);
    }
}
