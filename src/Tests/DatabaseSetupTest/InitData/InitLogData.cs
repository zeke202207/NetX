using FluentMigrator;

namespace DatabaseSetupTest;

/// <summary>
/// 初始化数据
/// </summary>
[Migration(20230816103500)]
public class InitLogData : Migration
{
    public override void Up()
    {
       Insert.IntoTable("Log")
           .Row(new { Id = 1, Text ="zeke" });
    }

    public override void Down()
    {
        Execute.Sql("delete * from log where id = 1");
    }
}
