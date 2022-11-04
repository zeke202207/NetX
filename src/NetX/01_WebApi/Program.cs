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
        app.UseMultiTenancy()
           .UserTenancyDatabase();
    })
    , "http://*:8220"
    );

public partial class Program { }