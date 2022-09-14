using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.DatabaseSetup.InitData;

[Migration(1004)]
public class InitUserData: InitDataMigration
{
    public InitUserData() 
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSER)
    {
    }

    public override void Up()
    {
        Insert.IntoTable(_tableName)
            .Row(new 
            { 
                id = "00000000000000000000000000000001", 
                username = "zeke",
                password="123456",
                nickname="zeke",
                avatar = "http://www.liuping.org.cn:8888/images/2020/03/07/1H3A4471-7-150x150.jpg",
                status=1,
                remark = "super admin"
            });
    }

    public override void Down()
    {
        Execute.Sql($"delete * from {_tableName}");
    }
}
