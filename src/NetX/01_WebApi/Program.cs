
using NetX.App;
using NetX.Tenants;

//TODO: 配置文件设置
ServerHost.Start(
    RunOption.Default
    .ConfigrationServiceCollection(services =>
    {
        //1.多租户设置
        services.AddTenancy(TenantType.Single)
                .WithDatabaseInfo(new DatabaseInfo()
                {
                    DatabaseHost = "www.liuping.org.cn",
                    DatabaseName = "mytestdb-zeke",
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
                })
                .WithTenancyDatabase()
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