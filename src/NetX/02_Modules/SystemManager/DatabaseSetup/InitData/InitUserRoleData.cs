using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.DatabaseSetup.InitData;

[Migration(1006)]
public class InitUserRoleData : InitDataMigration
{
    public InitUserRoleData()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSERROLE)
    {
    }

    public override void Up()
    {
        Insert.IntoTable(_tableName)
            .Row(new
            {
                userid = "00000000000000000000000000000001",
                roleid = "00000000000000000000000000000001"
            });
    }

    public override void Down()
    {
        Execute.Sql($"delete * from {_tableName}");
    }
}
