using FluentMigrator;
using NetX.DatabaseSetup;

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
               .WithColumn("id").AsString(50).PrimaryKey()
               .WithColumn("jobtaskid").AsString(50).PrimaryKey()
               .WithColumn("name").AsString(255).NotNullable()
               .WithColumn("cron").AsString(255).NotNullable()
               .WithColumn("triggertype").AsInt32().WithDefaultValue(0)
               .WithColumn("startat").AsDateTime().Nullable()
               .WithColumn("endat").AsDateTime().Nullable()
               .WithColumn("startnow").AsBoolean().WithDefaultValue(false)
               .WithColumn("priority").AsInt32().Nullable()
               .WithColumn("createtime").AsDateTime().WithDefaultValue(DateTime.Now)
               .WithColumn("description").AsString(500).Nullable();
    }
}
