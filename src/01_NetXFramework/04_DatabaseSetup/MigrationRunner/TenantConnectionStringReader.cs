using FluentMigrator.Runner.Initialization;
using NetX.Tenants;

namespace NetX.DatabaseSetup;

/// <summary>
/// 
/// </summary>
public class TenantConnectionStringReader : IConnectionStringReader
{
    /// <summary>
    /// 
    /// </summary>
    public int Priority => 1;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="connectionStringOrName"></param>
    /// <returns></returns>
    public string GetConnectionString(string connectionStringOrName)
    {
        return TenantContext.CurrentTenant.ConnectionStr ?? string.Empty;
    }
}
