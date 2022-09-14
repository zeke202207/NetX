using FluentMigrator;
using NetX.DatabaseSetup;
using NetX.SystemManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.DatabaseSetup.CreateTable;

[Migration(1)]
public class UserTable : CreateTableMigration
{
    public UserTable()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSER)
    {
    }

    public override void Up()
    {
        Create.Table(_tableName)
                .WithColumn("id").AsString().PrimaryKey()
                .WithColumn("username").AsString(50).NotNullable()
                .WithColumn("password").AsString(50).NotNullable()
                .WithColumn("nickname").AsString(255).Nullable()
                .WithColumn("avatar").AsString(500).Nullable()
                .WithColumn("status").AsInt16().Nullable().WithDefaultValue(1)
                .WithColumn("remark").AsString(500).Nullable();
    }
}
