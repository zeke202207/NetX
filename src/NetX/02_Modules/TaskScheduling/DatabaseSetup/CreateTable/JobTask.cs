using FluentMigrator;
using NetX.DatabaseSetup;

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
               .WithColumn("jobtype").AsString(255).NotNullable().WithColumnDescription("执行job的类型，用于反射")
               .WithColumn("datamap").AsString(500).Nullable()
               .WithColumn("disallowconcurrentexecution").AsBoolean().WithDefaultValue(false)
               .WithColumn("enabled").AsInt32().WithDefaultValue(0).WithColumnDescription("是否启用")
               .WithColumn("createtime").AsDateTime().WithDefaultValue(DateTime.Now)
               .WithColumn("description").AsString(500).Nullable();
    }
}
