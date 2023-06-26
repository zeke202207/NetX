using FluentMigrator.Infrastructure;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Constraints;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.MySql;
using FluentMigrator.Runner.Processors.SqlServer;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.Extensions.DependencyInjection;
using NetX.Cache.Core;
using NetX.Tenants;
using System.Collections.Concurrent;
using System.Reflection;

namespace NetX.DatabaseSetup;

/// <summary>
/// 数据迁移服务
/// </summary>
public class MigrationService : IMigrationService
{
    private readonly MigrationSupportDbType _supportDbType;
    private readonly ICacheProvider _cacheProvider;
    private readonly DbFactoryBase? _dbFactory;
    private readonly IMigrationRunner _runner;

    /// <summary>
    /// 数据迁移服务实例
    /// </summary>
    /// <param name="supportDbType"></param>
    /// <param name="cacheProvider"></param>
    /// <param name="dbFactories"></param>
    /// <param name="runner"></param>
    public MigrationService(
        MigrationSupportDbType supportDbType,
        ICacheProvider cacheProvider,
        IEnumerable<DbFactoryBase> dbFactories,
        IMigrationRunner runner)
    {
        this._supportDbType = supportDbType;
        this._cacheProvider = cacheProvider;
        this._dbFactory = GetDbFactoryBase(dbFactories);
        this._runner = runner;
    }

    /// <summary>
    /// Executes all found (and unapplied) migrations
    /// 创建数据库
    /// </summary>
    /// <returns></returns>
    public async Task<bool> MigrateUp()
    {
        return await MigrateUp(true);
    }

    public async Task<bool> MigrateUp(bool checkCache)
    {
        string cacheKey = string.Empty;
        if (checkCache)
        {
            cacheKey = GetCacheKey();
            if (string.IsNullOrWhiteSpace(cacheKey))
                return false;
            if (await _cacheProvider.ExistsAsync(cacheKey))
                return true;
            await _cacheProvider.SetAsync(cacheKey, TenantContext.CurrentTenant.Principal?.Tenant.TenantId);
        }
        var result = await CraeteDatabase() && MigrationTables();
        if (!result && checkCache)
            await _cacheProvider.RemoveAsync(cacheKey);
        return result;
    }

    /// <summary>
    ///  Migrate down to the given version
    /// </summary>
    /// <param name="version"></param>
    /// <returns></returns>
    public async Task<bool> MigrateDown(long version)
    {
        try
        {
            var cacheKey = GetCacheKey();
            if(string.IsNullOrWhiteSpace(cacheKey))
                return false;
            this._runner.MigrateDown(version);
            await _cacheProvider.RemoveAsync(cacheKey);
            return await Task.FromResult(true);
        }
        catch (Exception ex)
        {
            throw new Exception("版本回退失败", ex);
        }
    }

    /// <summary>
    /// 获取数据库连接工厂
    /// </summary>
    /// <param name="dbFactories"></param>
    /// <returns></returns>
    private DbFactoryBase? GetDbFactoryBase(IEnumerable<DbFactoryBase> dbFactories)
    {
        switch(this._supportDbType)
        {
            case MigrationSupportDbType.SqlServer:
                return dbFactories.FirstOrDefault(p => p.GetType().Equals(typeof(SqlServerDbFactory)));
            case MigrationSupportDbType.MySql5:
            default:
                return dbFactories.FirstOrDefault(p => p.GetType().Equals(typeof(MySqlDbFactory)));
        }
    }

    /// <summary>
    /// 创建数据库
    /// </summary>
    private async Task<bool> CraeteDatabase()
    {
        try
        {
            //创建数据库
            using (var conn = this._dbFactory?.Factory.CreateConnection())
            {
                if (null == conn)
                    return false;
                conn.ConnectionString = TenantContext.CurrentTenant.CreateSchemaConnectionStr;
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = QueryDatabaseSql();
                var result = await cmd.ExecuteScalarAsync();
                if (null != result)
                    return true;
                //创建数据库
                cmd.CommandText = CreateDatabaseSql();
                await cmd.ExecuteNonQueryAsync();
            }
            return true;

        }
        catch (Exception ex)
        {
            throw new Exception("创建数据库失败", ex);
        }
    }

    /// <summary>
    /// 获取数据库是否存在查询语句
    /// </summary>
    /// <returns></returns>
    private string QueryDatabaseSql()
    {
        switch (_supportDbType)
        {
            case MigrationSupportDbType.MySql5:
            default:
                return $"SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '{TenantContext.CurrentTenant.DatabaseName}';";
        }
    }

    /// <summary>
    /// 建库语句
    /// </summary>
    /// <returns></returns>
    private string CreateDatabaseSql()
    {
        switch (_supportDbType)
        {
            case MigrationSupportDbType.SqlServer:
                return $"CREATE DATABASE [{TenantContext.CurrentTenant.DatabaseName}]";
            case MigrationSupportDbType.MySql5:
            default:
                return $"CREATE DATABASE `{TenantContext.CurrentTenant.DatabaseName}` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci";
        }
    }

    /// <summary>
    /// 数据库迁移
    /// </summary>
    /// <returns></returns>
    private bool MigrationTables()
    {
        try
        {
            this._runner.MigrateUp();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("数据库迁移失败", ex);
        }
    }

    /// <summary>
    /// 获取迁移缓存key
    /// </summary>
    /// <returns></returns>
    private string GetCacheKey()
    {
        var tenantId = TenantContext.CurrentTenant.Principal?.Tenant.TenantId;
        if (string.IsNullOrWhiteSpace(tenantId))
            return tenantId;
        return $"{CacheKeys.DATABASESETUP_TENANT_ID}{tenantId}";
    }
}
