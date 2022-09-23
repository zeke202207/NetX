using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.MySql;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.Extensions.DependencyInjection;
using NetX.Tenants;
using System.Collections.Concurrent;

namespace NetX.DatabaseSetup;

/// <summary>
/// 数据迁移服务
/// </summary>
public class MigrationService
{
    private IServiceCollection _services;
    private readonly int _commandTimeout = 300;
    private readonly MigrationSupportDbType _supportDbType;
    private readonly HashSet<string> _migrationTenandIds = new HashSet<string>();

    /// <summary>
    /// 数据迁移服务实例
    /// </summary>
    /// <param name="services"></param>
    /// <param name="supportDbType"></param>
    public MigrationService(IServiceCollection services, MigrationSupportDbType supportDbType)
    {
        _services = services;
        _supportDbType = supportDbType;
    }

    /// <summary>
    /// 创建数据库
    /// </summary>
    /// <returns></returns>
    public bool SetupDatabase(string tenandId)
    {
        if (_migrationTenandIds.Contains(tenandId))
            return true;
        _migrationTenandIds.Add(tenandId);
        var result = CraeteDatabase() && MigrationTables();
        if(!result)
            _migrationTenandIds.Remove(tenandId);
        return result;
    }

    /// <summary>
    /// 创建数据库
    /// </summary>
    private bool CraeteDatabase()
    {
        try
        {
            var serviceProvider = _services.BuildServiceProvider(false);
            using (var scope = serviceProvider.CreateScope())
            {
                var dbFactory = GetDatabaseFactory(serviceProvider);
                //创建数据库
                using (var conn = dbFactory?.Factory.CreateConnection())
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
        }
        catch (Exception ex)
        {
            throw new Exception("创建数据库失败", ex);
        }
    }

    /// <summary>
    /// 获取数据库连接工厂
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    private ReflectionBasedDbFactory? GetDatabaseFactory(IServiceProvider serviceProvider)
    {
        if (null == serviceProvider)
            return null;
        switch (_supportDbType)
        {
            case MigrationSupportDbType.MySql5:
            default:
                return serviceProvider.GetService<MySqlDbFactory>();
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
        var reads = new List<IConnectionStringReader>() { new TenantConnectionStringReader() };
        var options = new TenantProcessorOptions();
        var selectingProcessorAccessor = new ConnectionStringAccessor(options, new TenantSelectingProcessorAccessorOptions(), reads);
        _services.AddScoped<IConnectionStringAccessor>(sp => { return selectingProcessorAccessor; });
        _services.AddScoped<IVersionTableMetaData>(sp => { return new TenantMigrationVersionTable(); });
        _services.ConfigureRunner(rb => rb.WithGlobalCommandTimeout(TimeSpan.FromSeconds(_commandTimeout)));
        var serviceProvider = _services.BuildServiceProvider(false);        
        using (var scope = serviceProvider.CreateScope())
        {
            var profileLoader = serviceProvider.GetService(typeof(IProfileLoader)) as IProfileLoader;
            var processorAccessor = serviceProvider.GetService(typeof(IProcessorAccessor)) as IProcessorAccessor;
            var maintenanceLoader = serviceProvider.GetService(typeof(IMaintenanceLoader)) as IMaintenanceLoader;
            var migrationLoader = serviceProvider.GetService(typeof(IMigrationInformationLoader)) as IMigrationInformationLoader;
            var migrationRunnerConventionsAccessor = serviceProvider.GetService(typeof(IMigrationRunnerConventionsAccessor)) as IMigrationRunnerConventionsAccessor;
            var runner = new MigrationRunner(
                new TenantRunnerOptions(),
                options,
                profileLoader,
                processorAccessor,
                maintenanceLoader,
                migrationLoader,
                new MigrationRunnerLogger(),
                new StopWatch(),
                migrationRunnerConventionsAccessor,
                new MigrationAssemblySource(),
                new MigrationValidator(new MigrationValidatorlogger(), new DefaultConventionSet()),
                serviceProvider
                );

            try
            {
                runner.MigrateUp();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("数据库迁移失败", ex);
            }
        }
    }
}
