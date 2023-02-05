using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.Tools.DatabaseSetup.CreateTable
{
    /// <summary>
    /// 系统日志表
    /// </summary>
    [Migration(20091126100601)]
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
        public override void Up()
        {
            base.IfDatabase("mysql").Create.Table(_tableName)
              .WithColumn("id").AsString(50).PrimaryKey()
              .WithColumn("threadid").AsString(50).Nullable()
              .WithColumn("eventid").AsString(255).Nullable()
              .WithColumn("name").AsString(255).Nullable()
              .WithColumn("level").AsInt16().Nullable()
              .WithColumn("elapsed").AsInt64().Nullable()
              .WithColumn("message").AsCustom("mediumtext").Nullable()
              .WithColumn("exception").AsCustom("mediumtext").Nullable()
              .WithColumn("state").AsCustom("mediumtext").Nullable()
              .WithColumn("createtime").AsDateTime().WithDefaultValue(DateTime.Now);

            base.IfDatabase("sqlserver").Create.Table(_tableName)
              .WithColumn("id").AsString(50).PrimaryKey()
              .WithColumn("threadid").AsString(50).Nullable()
              .WithColumn("eventid").AsString(255).Nullable()
              .WithColumn("name").AsString(255).Nullable()
              .WithColumn("level").AsInt16().Nullable()
              .WithColumn("elapsed").AsInt64().Nullable()
              .WithColumn("message").AsCustom("text").Nullable()
              .WithColumn("exception").AsCustom("text").Nullable()
              .WithColumn("state").AsCustom("text").Nullable()
              .WithColumn("createtime").AsDateTime().WithDefaultValue(DateTime.Now);
        }
    }
}
