using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.DatabaseSetup.CreateTable;

[Migration(5)]
public class RoleMenuTable : CreateTableMigration
{
    public RoleMenuTable()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLEMENU)
    {
    }

    public override void Up()
    {
        Create.Table(_tableName)
               .WithColumn("roleid").AsString(50)
               .ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLEMENU}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLE}", DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLE, "id")
               .WithColumn("menuid").AsString(50)
               .ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLEMENU}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSMENU}", DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSMENU, "id");
    }
}
