using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.RBAC.DatabaseSetup.CreateTable;

/// <summary>
/// 
/// </summary>
[Migration(20091124100607)]
public class UserRoleTable : CreateTableMigration
{
    /// <summary>
    /// 
    /// </summary>
    public UserRoleTable()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSERROLE)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Up()
    {
        Create.Table(_tableName)
               .WithColumn("userid").AsString(50).PrimaryKey()
               .ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSERROLE}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSER}", DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSER, "id")
               .WithColumn("roleid").AsString(50).PrimaryKey()
               .ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSERROLE}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLE}", DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLE, "id");
    }
}
