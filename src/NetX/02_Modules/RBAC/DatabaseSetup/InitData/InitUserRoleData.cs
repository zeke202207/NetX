using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.RBAC.DatabaseSetup.InitData;

/// <summary>
/// 
/// </summary>
[Migration(1006)]
public class InitUserRoleData : InitDataMigration
{
    /// <summary>
    /// 
    /// </summary>
    public InitUserRoleData()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSERROLE)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Up()
    {
        Insert.IntoTable(_tableName)
            .Row(new
            {
                userid = "00000000000000000000000000000001",
                roleid = "00000000000000000000000000000001"
            });
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Down()
    {
        Execute.Sql($"delete * from {_tableName}");
    }
}
