using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.RBAC.DatabaseSetup.CreateTable;

/// <summary>
/// 
/// </summary>
[Migration(20091124100604)]
public class RoleTable : CreateTableMigration
{
    /// <summary>
    /// 
    /// </summary>
    public RoleTable()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLE)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Up()
    {
        Create.Table(_tableName)
               .WithColumn("id").AsString(50).PrimaryKey()
               .WithColumn("rolename").AsString(50).NotNullable()
               .WithColumn("status").AsInt16().Nullable().WithDefaultValue(1)
               .WithColumn("apicheck").AsInt16().Nullable().WithDefaultValue(0)
               .WithColumn("createtime").AsDateTime().WithDefaultValue(DateTime.Now)
               .WithColumn("remark").AsString(500).Nullable();
    }
}
