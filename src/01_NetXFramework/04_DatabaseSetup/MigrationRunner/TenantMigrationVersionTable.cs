using FluentMigrator.Runner.VersionTableInfo;
using NetX.Tenants;

namespace NetX.DatabaseSetup;

/// <summary>
/// 
/// </summary>
[VersionTableMetaData]
public class TenantMigrationVersionTable : IVersionTableMetaData
{
    /// <summary>
    /// 
    /// </summary>
    public TenantMigrationVersionTable()
    {
        SchemaName = TenantContext.CurrentTenant.DatabaseName;
    }

    /// <summary>
    /// 
    /// </summary>
    public string SchemaName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string TableName => "VersionInfo";

    /// <summary>
    /// 
    /// </summary>
    public string ColumnName => "Version";

    /// <summary>
    /// 
    /// </summary>
    public string UniqueIndexName => "UC_Version";

    /// <summary>
    /// 
    /// </summary>
    public string AppliedOnColumnName => "AppliedOn";

    /// <summary>
    /// 
    /// </summary>
    public string DescriptionColumnName => "Description";

    /// <summary>
    /// 
    /// </summary>
    public bool OwnsSchema => true;

    /// <summary>
    /// 
    /// </summary>
    public object? ApplicationContext { get; set; }

}
