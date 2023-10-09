using Google.Protobuf.Reflection;
using NetX.App;
using NetX.Tenants;
using ServiceSelf;

var serviceOption = new NetX.App.Options.ServiceOption()
{
    ServiceName = "netx",
    Options = new ServiceSelf.ServiceOptions()
    {
        Description = "netx",
    }
};
serviceOption.Options.Windows.DisplayName = "netx";
serviceOption.Options.Windows.FailureActionType = WindowsServiceActionType.Restart;
serviceOption.Options.Linux.Service.Restart = "always";
serviceOption.Options.Linux.Service.RestartSec = "10";

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
    , "http://*:8220",
    serviceOption
    );

/// <summary>
/// 声明progrom类
/// 主要用于接口mock测试
/// </summary>
public partial class Program { }