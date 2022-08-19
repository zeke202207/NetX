#define NetX

#if !NetX
using NetX.TestWebApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IZeke, Zeke>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
#else

using NetX;
using NetX.Module;
using NetX.MutilTenant;
using NetX.TestWebApi.Controllers;
using System.Reflection;

ServerHost.Start(
    RunOption.Default
    .ConfigrationServiceCollection(services =>
    {
        services.AddScoped<IZeke, Zeke>();
        //1.多租户设置
        services.AddTenancy(TenantType.Multi)
                .WithDatabaseInfo(new DatabaseInfo()
                {
                    DatabaseHost = "www.liuping.org.cn",
                    DatabaseName = "mytestdb",
                    DatabasePort = 8306,
                    DatabaseType = DatabaseType.MySql,
                    UserId = "root",
                    Password = "root"
                })
                .WithResolutionStrategy<HostResolutionStrategy>()
                .WithStore<InMemoryTenantStore>()
                .WithPerTenantOptions<CookiePolicyOptions>((options, tenant) =>
                {
                    options.ConsentCookie.Name = tenant.TenantId + "-consent";
                });
    })
    .ConfigApplication(app =>
    {
        //1.多租户
        app.UseMultiTenancy();
    })
    , "http://*:8220"
    );
#endif