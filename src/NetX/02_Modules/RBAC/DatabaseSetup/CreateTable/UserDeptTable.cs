using FluentMigrator;
using NetX.DatabaseSetup;
using NetX.RBAC.Models;

namespace NetX.RBAC.DatabaseSetup.CreateTable;

/// <summary>
/// 
/// </summary>
[Migration(20091124100606)]
public class UserDeptTable : CreateTableMigration
{
    /// <summary>
    /// 
    /// </summary>
    public UserDeptTable()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSERDEPT)
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
                    .WithColumn(nameof(sys_user_dept.userid).ToLower()).AsString(50).PrimaryKey()
                    //.ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSERDEPT}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSER}", DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSER, "id")
                    .WithColumn(nameof(sys_user_dept.deptid).ToLower()).AsString(50).PrimaryKey()
                    //.ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSERDEPT}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSDEPT}", DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSDEPT, "id")
                    ;
            Create.Index("idx_userdept")
                .OnTable(_tableName)
                .OnColumn("userid").Ascending()
                .OnColumn("deptid").Ascending();
        }
        catch (Exception ex)
        {
        }
    }
}
