using NetX.Ddd.Core;
using NetX.Module;
using NetX.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Ddd.Domain;

public abstract class BaseDbContext: DbContext
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
