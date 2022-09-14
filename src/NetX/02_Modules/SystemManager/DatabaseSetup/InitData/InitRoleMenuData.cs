using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.DatabaseSetup.InitData;

[Migration(1005)]
public class InitRoleMenuData : InitDataMigration
{
    public InitRoleMenuData()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLEMENU)
    {
    }

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
                menuid = "00000000000000000000000000000002"
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
            });
    }

    public override void Down()
    {
        Execute.Sql($"delete * from {_tableName}");
    }
}
