using NetX.App;
using NetX.Tenants;

ServerHost.Start(
    RunOption.Default
    .ConfigrationServiceCollection((services, config) =>
    {
        //1.多租户设置
        services.AddTenancy(TenantType.Single)
                .WithTenancyDatabase(config)
                .WithResolutionStrategy<HostResolutionStrategy>()
                .WithStore<InMemoryTenantStore>()
                .Build();
    })
    .ConfigApplication(app =>
    {
        //1.多租户
        app.UseMultiTenancy()
           .UserTenancyDatabase();
    })
    , "http://*:8220"
    );