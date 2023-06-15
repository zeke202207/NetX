using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.TaskScheduling.DatabaseSetup;

/// <summary>
/// 
/// </summary>
[Migration(20091224100603)]
public class JobTaskTrigger : CreateTableMigration
{
    /// <summary>
    /// 
    /// </summary>
    public JobTaskTrigger()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSJOBTASK_TRIGGER)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Up()
    {
        Create.Table(_tableName)
               .WithColumn("jobtaskid").AsString(50).PrimaryKey()
               .ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSJOBTASK_TRIGGER}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSJOBTASK}", DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSJOBTASK, "id")
               .WithColumn("triggerid").AsString(255).NotNullable()
               .ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSJOBTASK_TRIGGER}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYS_TRIGGER}", DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYS_TRIGGER, "id");
    }
}
