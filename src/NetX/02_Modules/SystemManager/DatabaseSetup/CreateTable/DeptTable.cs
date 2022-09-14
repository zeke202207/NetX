using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.DatabaseSetup.CreateTable;

[Migration(2)]
public class DeptTable : CreateTableMigration
{
    public DeptTable() 
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSDEPT)
    {
    }

    public override void Up()
    {
        Create.Table(_tableName)
               .WithColumn("id").AsString(50).PrimaryKey()
               .WithColumn("parentid").AsString(50).NotNullable()
               .WithColumn("deptname").AsString(255)
               .WithColumn("orderno").AsInt32()
               .WithColumn("createtime").AsDate().WithDefaultValue(DateTime.Now)
               .WithColumn("status").AsInt16().Nullable().WithDefaultValue(1)
               .WithColumn("remark").AsString(500).Nullable();
    }
}
