using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.RBAC.DatabaseSetup.InitData;

/// <summary>
/// 
/// </summary>
[Migration(1005)]
public class InitRoleMenuData : InitDataMigration
{
    /// <summary>
    /// 
    /// </summary>
    public InitRoleMenuData()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLEMENU)
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
                roleid = "00000000000000000000000000000001",
                menuid = "00000000000000000000000000000001"
            })
            .Row(new
            {
                roleid = "00000000000000000000000000000001",
                menuid = "00000000000000000000000000000003"
            })
            .Row(new
            {
                roleid = "00000000000000000000000000000001",
                menuid = "00000000000000000000000000000004"
            })
            .Row(new
            {
                roleid = "00000000000000000000000000000001",
                menuid = "00000000000000000000000000000005"
            })
            .Row(new
            {
                roleid = "00000000000000000000000000000001",
                menuid = "00000000000000000000000000000006"
            })
            .Row(new
            {
                roleid = "00000000000000000000000000000001",
                menuid = "00000000000000000000000000000007"
            })
            .Row(new
            {
                roleid = "00000000000000000000000000000001",
                menuid = "00000000000000000000000000000008"
            })
            .Row(new
            {
                roleid = "00000000000000000000000000000001",
                menuid = "00000000000000000000000000000010"
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
