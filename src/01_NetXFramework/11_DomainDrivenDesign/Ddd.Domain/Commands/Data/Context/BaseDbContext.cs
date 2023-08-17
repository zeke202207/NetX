using NetX.Tenants;

namespace NetX.Ddd.Domain;

public abstract class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions options)
       : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (null != TenantContext.CurrentTenant)
        {
            if (TenantContext.CurrentTenant.TenantOption?.DatabaseInfo?.DatabaseType == DatabaseType.MySql)
                optionsBuilder.UseMySql(TenantContext.CurrentTenant.ConnectionStr, new MySqlServerVersion(new Version(5, 7, 37)));
        }
        base.OnConfiguring(optionsBuilder);
    }
}
