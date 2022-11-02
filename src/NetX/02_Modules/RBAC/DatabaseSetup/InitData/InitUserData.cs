using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.RBAC.DatabaseSetup.InitData;

/// <summary>
/// 
/// </summary>
[Migration(1004)]
public class InitUserData : InitDataMigration
{
    /// <summary>
    /// 
    /// </summary>
    public InitUserData()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSER)
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
                id = "00000000000000000000000000000001",
                username = "zeke",
                password = "E10ADC3949BA59ABBE56E057F20F883E",
                nickname = "zeke",
                avatar = "http://www.liuping.org.cn:8888/images/2020/03/07/1H3A4471-7-150x150.jpg",
                status = 1,
                remark = "super admin"
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
