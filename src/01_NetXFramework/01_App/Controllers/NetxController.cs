using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.DatabaseSetup;
using NetX.Logging.Monitors;
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
public class NetxController: ApiBaseController
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
    [ApiActionDescription("迁移数据库")]
    [NoPermission]
    [SuppressMonitor]
    [HttpPost]
    public async Task<bool> MigrateUp()
    {
        return await _migrationService.MigrateUp();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("迁移数据库-将删除表或者数据，请谨慎操作(尽量不要在生产环境中使用)")]
    [NoPermission]
    [SuppressMonitor]
    [HttpPost]
    public async Task<bool> MigrateDown(long version)
    {
        return await _migrationService.MigrateDown(version);
    }
}
