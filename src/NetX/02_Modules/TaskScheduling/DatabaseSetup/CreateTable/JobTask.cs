using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.DatabaseSetup;

/// <summary>
/// 
/// </summary>
[Migration(20091224100601)]
public class JobTask : CreateTableMigration
{
    /// <summary>
    /// 
    /// </summary>
    public JobTask()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSJOBTASK)
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
               .WithColumn("group").AsString(255).NotNullable()
               .WithColumn("jobtype").AsString(255).NotNullable()
               .WithColumn("datamap").AsString(500).Nullable()
               .WithColumn("disallowconcurrentexecution").AsBoolean().WithDefaultValue(false)
               .WithColumn("createtime").AsDateTime().WithDefaultValue(DateTime.Now)
               .WithColumn("description").AsString(500).Nullable();
    }
}
