using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.DatabaseSetup.InitData;

[Migration(9001)]
public class InitDebuggerData : InitDataMigration
{
    public InitDebuggerData()
       : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSDEPT)
    {
    }

    public override void Up()
    {
#if DEBUG
        Insert.IntoTable(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSDEPT)
            .Row(new
            {
                id = "00000000000000000000000000000001",
                parentid = "00000000000000000000000000000000",
                deptname = "dept1",
                orderno = 1,
                createtime = DateTime.Now,
                status = 1,
                remark = "测试数据"
            })
            .Row(new
            {
                id = "00000000000000000000000000000002",
                parentid = "00000000000000000000000000000000",
                deptname = "dept2",
                orderno = 2,
                createtime = DateTime.Now,
                status = 0,
                remark = "测试数据"
            })
            .Row(new
            {
                id = "00000000000000000000000000000003",
                parentid = "00000000000000000000000000000001",
                deptname = "dept1-1",
                orderno = 1,
                createtime = DateTime.Now,
                status = 0,
                remark = "测试数据"
            });

        Insert.IntoTable(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSERDEPT)
            .Row(new
            {
                userid= "00000000000000000000000000000001",
                deptid= "00000000000000000000000000000003"
            });

#endif
    }

    public override void Down()
    {
        Execute.Sql($"delete * from {_tableName}");
    }
}
