using FluentMigrator;
using NetX.DatabaseSetup;
using NetX.TaskScheduling.Model;

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
               .WithColumn(nameof(sys_jobtask.Id).ToLower()).AsString(50).PrimaryKey()
               .WithColumn(nameof(sys_jobtask.name).ToLower()).AsString(255).NotNullable()
               .WithColumn(nameof(sys_jobtask.group).ToLower()).AsString(255).NotNullable()
               .WithColumn(nameof(sys_jobtask.jobtype).ToLower()).AsString(255).NotNullable().WithColumnDescription("执行job的类型，用于反射")
               .WithColumn(nameof(sys_jobtask.datamap).ToLower()).AsString(500).Nullable()
               .WithColumn(nameof(sys_jobtask.disallowconcurrentexecution).ToLower()).AsBoolean().WithDefaultValue(false)
               .WithColumn(nameof(sys_jobtask.enabled).ToLower()).AsInt32().WithDefaultValue(0).WithColumnDescription("是否启用 0->启用")
               .WithColumn(nameof(sys_jobtask.state).ToLower()).AsInt32().WithDefaultValue(0).WithColumnDescription("任务运行状态： 0->None 1->Started 2->Paused 3->Resumed 4->Deleted 5->Interrupted")
               .WithColumn(nameof(sys_jobtask.createtime).ToLower()).AsDateTime().WithDefaultValue(DateTime.Now)
               .WithColumn(nameof(sys_jobtask.description).ToLower()).AsString(500).Nullable();
    }
}
