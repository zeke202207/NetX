using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.DatabaseSetup.CreateTable;

[Migration(6)]
public class UserDeptTable : CreateTableMigration
{
    public UserDeptTable()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSERDEPT)
    {
    }

    public override void Up()
    {
        Create.Table(_tableName)
                .WithColumn("userid").AsString(50)
                .ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSERDEPT}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSER}",DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSER, "id")
                .WithColumn("deptid").AsString(50)
                .ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSERDEPT}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSDEPT}", DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSDEPT, "id");
    }
}
