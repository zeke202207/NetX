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
using NetX.TestWebApi.Controllers;
using System.Reflection;

ServerHost.Start(
    RunOption.Default.ConfigrationServiceCollection(p => p.AddScoped<IZeke, Zeke>())
    );
#endif