using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.SystemManager.DatabaseSetup.CreateTable;

/// <summary>
/// 
/// </summary>
[Migration(5)]
public class RoleMenuTable : CreateTableMigration
{
    /// <summary>
    /// 
    /// </summary>
    public RoleMenuTable()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLEMENU)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Up()
    {
        Create.Table(_tableName)
               .WithColumn("roleid").AsString(50).PrimaryKey()
               .ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLEMENU}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLE}", DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLE, "id")
               .WithColumn("menuid").AsString(50).PrimaryKey()
               .ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLEMENU}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSMENU}", DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSMENU, "id");
    }
}
