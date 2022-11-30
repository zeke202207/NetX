using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.DatabaseSetup.CreateTable;


/// <summary>
/// 
/// </summary>
[Migration(20091224100602)]
public class Trigger : CreateTableMigration
{
    /// <summary>
    /// 
    /// </summary>
    public Trigger()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYS_TRIGGER)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Up()
    {
        Create.Table(_tableName)
               .WithColumn("id").AsString(50).PrimaryKey()
               .WithColumn("name").AsString(255).NotNullable()
               .WithColumn("startat").AsDateTime().Nullable()
               .WithColumn("endat").AsDate().Nullable()
               .WithColumn("startnow").AsBoolean().WithDefaultValue(false)
               .WithColumn("priority").AsInt32().Nullable()
               .WithColumn("createtime").AsDateTime().WithDefaultValue(DateTime.Now)
               .WithColumn("description").AsString(500).Nullable();
    }
}
