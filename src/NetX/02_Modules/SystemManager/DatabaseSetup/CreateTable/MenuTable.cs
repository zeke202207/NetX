using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.DatabaseSetup.CreateTable;

[Migration(3)]
public class MenuTable : CreateTableMigration
{
    public MenuTable()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSMENU)
    {
    }

    public override void Up()
    {
        Create.Table(_tableName)
               .WithColumn("id").AsString().PrimaryKey()
               .WithColumn("parentid").AsString(50).NotNullable()
               .WithColumn("icon").AsString(50).Nullable()
               .WithColumn("type").AsInt16().WithDefaultValue(0)
               .WithColumn("orderno").AsInt32()
               .WithColumn("permission").AsString(50).Nullable()
               .WithColumn("createtime").AsDateTime().WithDefaultValue(DateTime.Now)
               .WithColumn("name").AsString(50)
               .WithColumn("path").AsString(255).Nullable()
               .WithColumn("component").AsString(255).Nullable()
               .WithColumn("redirect").AsString(255).Nullable()
               .WithColumn("status").AsInt16().WithDefaultValue(1)
               .WithColumn("isext").AsInt16().WithDefaultValue(1)
               .WithColumn("keepalive").AsInt16().WithDefaultValue(1)
               .WithColumn("show").AsInt16().WithDefaultValue(1)
               .WithColumn("meta").AsString(500).Nullable();
    }
}
