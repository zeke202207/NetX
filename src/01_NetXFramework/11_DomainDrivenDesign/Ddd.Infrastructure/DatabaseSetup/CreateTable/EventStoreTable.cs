using FluentMigrator;
using NetX.DatabaseSetup;

namespace Netx.Ddd.Infrastructure.DatabaseSetup;

/// <summary>
/// api资源表
/// </summary>
[Migration(20091220100608)]
public class EventStoreTable : CreateTableMigration
{
    /// <summary>
    /// 
    /// </summary>
    public EventStoreTable()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_EVENTSTORE)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Up()
    {
        Create.Table(_tableName)
               .WithColumn("id").AsString(50).PrimaryKey()
               .WithColumn("aggregateid").AsString(255).NotNullable()
               .WithColumn("messagetype").AsString(255).NotNullable()
               .WithColumn("data").AsString(5000).NotNullable()
               .WithColumn("userid").AsString(50).NotNullable()
               .WithColumn("createtime").AsDateTime().WithDefaultValue(DateTime.Now);
    }
}
