using FluentMigrator;

namespace DatabaseSetupTest;

/// <summary>
/// 建表
/// </summary>
[Migration(20220816103500)]
public class AddLogTable : Migration
{
    public override void Up()
    {
        Create.Table("Log")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Text").AsString();
    }

    public override void Down()
    {
        Delete.Table("Log");
    }
}