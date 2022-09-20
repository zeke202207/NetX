using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.DatabaseSetup.InitData;

[Migration(1003)]
public class InitRoleData : InitDataMigration
{
    public InitRoleData() 
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLE)
    {
    }

    public override void Down()
    {
        Execute.Sql($"delete * from {_tableName}");
    }

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
