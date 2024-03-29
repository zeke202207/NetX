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


using NetX.App;
using NetX.Tenants;
using WebApiDemo;

ServerHost.Start(
    RunOption.Default
    .ConfigrationServiceCollection((services,config) =>
    {
        services.AddScoped<IZeke, Zeke>();
        //1.多租户设置
        services.AddTenancy(config)
                .Build();
    })
    .ConfigApplication(app =>
    {
        //1.多租户
        app.UseMultiTenancy();
    })
    , "http://*:8220"
    );
#endif