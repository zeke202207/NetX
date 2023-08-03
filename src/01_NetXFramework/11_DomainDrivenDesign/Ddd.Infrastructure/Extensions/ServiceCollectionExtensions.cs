using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetX.DatabaseSetup;
using NetX.Tenants;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Ddd.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainDrivenDesign(this IServiceCollection services, IConfiguration config)
    {
        var database = config.GetSection(TenantConst.C_TENANT_CONFIG_DATABASEINFO).Get<DatabaseInfo>();
        services.AddScoped<IMediator, Mediator>()
            .AddTransient<ServiceFactory>(sp => sp.GetService)
            .AddScoped<ICommandBus, CommandBus>()
            .AddScoped<IQueryBus, QueryBus>();
        services.TryAddScoped<IEventBus, EventStoreEvent>();
        services.TryAddScoped<IEventStoreRepository, EventStoreSQLRepository>();
        services.ConfigDatabase(database);
        //CodeFirst
        services.AddMigratorAssembly(new Assembly[] { Assembly.GetExecutingAssembly() }, MigrationSupportDbType.MySql5);
        services.AddScoped<IDatabaseContext, DapperContext>();
        return services;
    }

    private static IServiceCollection ConfigDatabase(this IServiceCollection services, DatabaseInfo info)
    {
        if (info == null)
            return services;
        var strConn = "service=";
        switch(info.DatabaseType)
        {
            case NetX.Tenants.DatabaseType.MySql:
                services.AddDbContext<NetxContext>(options =>options.UseMySql(strConn, new MySqlServerVersion(new Version(5, 7, 37))));
                services.AddDbContext<EventStoreSQLContext>(options =>options.UseMySql(strConn, new MySqlServerVersion(new Version(5, 7, 37))));
                break;
            default:
                throw new NotSupportedException("this database is not support!");
        }
        services.AddTransient<NetxContext>();
        services.AddTransient<EventStoreSQLContext>();
        services.AddScoped<IUnitOfWork,UnitOfWork>();
        return services;
    }
}
