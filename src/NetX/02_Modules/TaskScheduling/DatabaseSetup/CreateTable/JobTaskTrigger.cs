using FluentMigrator;
using NetX.DatabaseSetup;
using NetX.TaskScheduling.Model;

namespace NetX.TaskScheduling.DatabaseSetup.CreateTable;


/// <summary>
/// 
/// </summary>
[Migration(20091224100602)]
public class JobTaskTrigger : CreateTableMigration
{
    /// <summary>
    /// 
    /// </summary>
    public JobTaskTrigger()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYS_TRIGGER)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Up()
    {
        Create.Table(_tableName)
               .WithColumn(nameof(sys_jobtasktrigger.Id).ToLower()).AsString(50).PrimaryKey()
               .WithColumn(nameof(sys_jobtasktrigger.jobtaskid).ToLower()).AsString(50).PrimaryKey()
               .WithColumn(nameof(sys_jobtasktrigger.name).ToLower()).AsString(255).NotNullable()
               .WithColumn(nameof(sys_jobtasktrigger.cron).ToLower()).AsString(255).NotNullable()
               .WithColumn(nameof(sys_jobtasktrigger.triggertype).ToLower()).AsInt32().WithDefaultValue(0)
               .WithColumn(nameof(sys_jobtasktrigger.startat).ToLower()).AsDateTime().Nullable()
               .WithColumn(nameof(sys_jobtasktrigger.endat).ToLower()).AsDateTime().Nullable()
               .WithColumn(nameof(sys_jobtasktrigger.startnow).ToLower()).AsBoolean().WithDefaultValue(false)
               .WithColumn(nameof(sys_jobtasktrigger.priority).ToLower()).AsInt32().Nullable()
               .WithColumn(nameof(sys_jobtasktrigger.createtime).ToLower()).AsDateTime().WithDefaultValue(DateTime.Now)
               .WithColumn(nameof(sys_jobtasktrigger.description).ToLower()).AsString(500).Nullable();
    }
}
