using FluentMigrator;

namespace DatabaseSetupTest;

/// <summary>
/// 建库
/// </summary>
[Migration(20220816103400)]
public class AddTable1 : Migration
{
    public override void Up()
    {
        //demo 
        //Create.Table("TestTable").WithColumn("Name").AsString().Nullable().WithDefaultValue(RawSql.Insert("SYSUTCDATETIME()"));
        Create.Table("Log1")
           .WithColumn("Id").AsInt64().PrimaryKey().Identity()
           .WithColumn("Text").AsString();
    }

    public override void Down()
    {
        Delete.Table("Log1");
    }
}

