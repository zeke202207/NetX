using NetX.App;
using NetX.Tenants;

ServerHost.Start(
    RunOption.Default
    .ConfigrationServiceCollection((services, config) =>
    {
        //1.���⻧����
        services.AddTenancy(config).Build();
    })
    .ConfigApplication(app =>
    {
        //1.���⻧
        app.UseMultiTenancy();
    })
    , "http://*:8220"
    );

/// <summary>
/// ����progrom��
/// ��Ҫ���ڽӿ�mock����
/// </summary>
public partial class Program { }