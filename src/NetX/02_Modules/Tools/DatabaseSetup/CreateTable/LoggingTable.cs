using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.Tools.DatabaseSetup.CreateTable
{
    /// <summary>
    /// 系统日志表
    /// </summary>
    [Migration(2001)]
    public class LoggingTable : CreateTableMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public LoggingTable()
            : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_LOGGING)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Up()
        {
            Create.Table(_tableName)
              .WithColumn("id").AsString(50).PrimaryKey()
              .WithColumn("threadid").AsString(50).Nullable()
              .WithColumn("eventid").AsString(255).Nullable()
              .WithColumn("name").AsString(255).NotNullable()
              .WithColumn("level").AsInt16().NotNullable()
              .WithColumn("message").AsCustom("text").Nullable()
              .WithColumn("exception").AsCustom("text").Nullable()
              .WithColumn("context").AsCustom("text").Nullable()
              .WithColumn("state").AsCustom("text").Nullable()
              .WithColumn("createtime").AsDateTime().WithDefaultValue(DateTime.Now);
        }
    }
}
