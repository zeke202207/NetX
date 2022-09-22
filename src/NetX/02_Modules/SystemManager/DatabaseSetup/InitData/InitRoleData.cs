using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.SystemManager.DatabaseSetup.InitData;

/// <summary>
/// 
/// </summary>
[Migration(1003)]
public class InitRoleData : InitDataMigration
{
    /// <summary>
    /// 
    /// </summary>
    public InitRoleData()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLE)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Down()
    {
        Execute.Sql($"delete * from {_tableName}");
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Up()
    {
        Insert.IntoTable(_tableName)
                .Row(new
                {
                    id = "00000000000000000000000000000001",
                    rolename = "super admin",
                    status = (int)Status.Enable,
                    createtime = DateTime.Now,
                    remark = ""
                });
    }
}
