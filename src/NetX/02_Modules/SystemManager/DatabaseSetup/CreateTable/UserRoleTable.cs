using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.DatabaseSetup.CreateTable;

[Migration(7)]
public class UserRoleTable : CreateTableMigration
{
    public UserRoleTable()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSERROLE)
    {
    }

    public override void Up()
    {
        Create.Table(_tableName)
               .WithColumn("userid").AsString(50).PrimaryKey()
               .ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSERROLE}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSER}", DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSER, "id")
               .WithColumn("roleid").AsString(50).PrimaryKey()
               .ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSERROLE}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLE}", DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLE, "id");
    }
}
