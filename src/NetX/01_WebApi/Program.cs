using NetX.App;
using NetX.Tenants;

ServerHost.Start(
    RunOption.Default
    .ConfigrationServiceCollection((services, config) =>
    {
        //1.多租户设置
        services.AddTenancy(config).Build();
    })
    .ConfigApplication(app =>
    {
        //1.多租户
        app.UseMultiTenancy();
    })
    , "http://*:8220"
    );

/// <summary>
/// 声明progrom类
/// 主要用于接口mock测试
/// </summary>
public partial class Program { }