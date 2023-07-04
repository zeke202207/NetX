using FluentMigrator;
using NetX.DatabaseSetup;
using NetX.RBAC.Models;

namespace NetX.RBAC.DatabaseSetup.CreateTable;

/// <summary>
/// 
/// </summary>
[Migration(20091124100605)]
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
        try
        {
            Create.Table(_tableName)
                   .WithColumn(nameof(sys_role_menu.roleid).ToLower()).AsString(50).PrimaryKey()
                   //.ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLEMENU}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLE}", DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLE, "id")
                   .WithColumn(nameof(sys_role_menu.menuid).ToLower()).AsString(50).PrimaryKey()
                   //.ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLEMENU}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSMENU}", DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSMENU, "id")
                   ;
        }
        catch (Exception ex)
        {
        }
    }
}
