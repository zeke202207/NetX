using NetX.App;
using NetX.Tenants;

ServerHost.Start(
    RunOption.Default
    .ConfigrationServiceCollection((services, config) =>
    {
        //1.���⻧����
        services.AddTenancy(TenantType.Single)
                .WithTenancyDatabase(config)
                .WithResolutionStrategy<HostResolutionStrategy>()
                .WithStore<InMemoryTenantStore>()
                .Build();
    })
    .ConfigApplication(app =>
    {
        //1.���⻧
        app.UseMultiTenancy()
           .UserTenancyDatabase();
    })
    , "http://*:8220"
    );