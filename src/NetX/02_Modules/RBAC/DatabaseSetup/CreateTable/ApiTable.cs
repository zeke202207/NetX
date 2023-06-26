using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.RBAC.DatabaseSetup.CreateTable;

/// <summary>
/// api资源表
/// </summary>
[Migration(20091124100608)]
public class ApiTable : CreateTableMigration
{
    /// <summary>
    /// 
    /// </summary>
    public ApiTable()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSAPI)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Up()
    {

        try
        {
            Create.Table(_tableName)
                   .WithColumn("id").AsString(50).PrimaryKey()
                   .WithColumn("path").AsString(255).NotNullable()
                   .WithColumn("group").AsString(255).NotNullable()
                   .WithColumn("method").AsString(50).NotNullable()
                   .WithColumn("createtime").AsDateTime().WithDefaultValue(DateTime.Now)
                   .WithColumn("description").AsString(500).Nullable();
        }
        catch (Exception ex)
        {
        }
    }
}
