using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.DatabaseSetup.CreateTable;

 [Migration(4)]
public class RoleTable : CreateTableMigration
{
    public RoleTable()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLE)
    {
    }

    public override void Up()
    {
        Create.Table(_tableName)
               .WithColumn("id").AsString().PrimaryKey()
               .WithColumn("rolename").AsString(50).NotNullable()
               .WithColumn("rolevalue").AsString(50)
               .WithColumn("status").AsInt16().Nullable().WithDefaultValue(1)
               .WithColumn("orderno").AsInt16()
               .WithColumn("createtime").AsDateTime().WithDefaultValue(DateTime.Now)
               .WithColumn("remark").AsString(500).Nullable();
    }
}
