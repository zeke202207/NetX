using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.RBAC.DatabaseSetup.InitData;

/// <summary>
/// 
/// </summary>
[Migration(20091125100603)]
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
        try
        {
            Execute.Sql($"delete from {_tableName}");
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Up()
    {
        try
        {
            Insert.IntoTable(_tableName)
                    .Row(new
                    {
                        id = "00000000000000000000000000000001",
                        rolename = "super admin",
                        status = (int)Status.Enable,
                        createtime = DateTime.Now,
                        remark = "",
                        issystem = true,
                    });
        }
        catch (Exception ex)
        {
        }
    }
}
