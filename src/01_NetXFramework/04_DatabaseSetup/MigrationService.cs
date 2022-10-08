using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.MySql;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.Extensions.DependencyInjection;
using NetX.Cache.Core;
using NetX.Tenants;
using System.Collections.Concurrent;

namespace NetX.DatabaseSetup;

/// <summary>
/// 数据迁移服务
/// </summary>
public class MigrationService
{
    private readonly MigrationSupportDbType _supportDbType;
    private readonly ICacheProvider _cacheProvider;
    private readonly DbFactoryBase _dbFactory;
    private IMigrationRunner _runner;

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
    /// 创建数据库
    /// </summary>
    /// <returns></returns>
    public bool SetupDatabase(string tenandId)
    {
        if (_cacheProvider.Exists(CacheKeys.DATABASESETUP_TENANT_ID))
            return true;
        _cacheProvider.Set(CacheKeys.DATABASESETUP_TENANT_ID, tenandId);
        var result = CraeteDatabase() && MigrationTables();
        if (!result)
            _cacheProvider.Remove(tenandId);
        return result;
    }

    /// <summary>
    /// 获取数据库连接工厂
    /// </summary>
    /// <param name="dbFactories"></param>
    /// <returns></returns>
    private DbFactoryBase GetDbFactoryBase(IEnumerable<DbFactoryBase> dbFactories)
    {
        switch(this._supportDbType)
        {
            case MigrationSupportDbType.MySql5:
            default:
                return dbFactories.FirstOrDefault(p => p.GetType().Equals(typeof(MySqlDbFactory)));
        }
    }

    /// <summary>
    /// 创建数据库
    /// </summary>
    private bool CraeteDatabase()
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
                var result = cmd.ExecuteScalar();
                if (null != result)
                    return true;
                //创建数据库
                cmd.CommandText = CreateDatabaseSql();
                cmd.ExecuteNonQuery();
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
}
