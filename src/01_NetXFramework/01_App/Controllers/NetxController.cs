using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.Common.Models;
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
    private readonly MigrationService _migrationService;

    /// <summary>
    /// 框架服务接口
    /// </summary>
    /// <param name="migrationService"></param>
    public NetxController(MigrationService migrationService)
    {
        this._migrationService = migrationService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tenantId">租户唯一标识</param>
    /// <returns></returns>
    [ApiActionDescription("迁移数据库")]
    [NoPermission]
    [SuppressMonitor]
    [HttpPost]
    [Obsolete]
    public async Task<bool> DatabaseUp(string tenantId)
    {
        return await Task.FromResult(_migrationService.SetupDatabase(tenantId));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("迁移数据库")]
    [NoPermission]
    [SuppressMonitor]
    [HttpPost]
    [Obsolete("暂不提供")]
    public async Task<bool> DatabaseDown(string tenantId)
    {
        return await Task.FromResult(true);
    }
}
