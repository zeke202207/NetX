using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.LogCollector.DatabaseSetup.CreateTable
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
              .WithColumn("name").AsString(255).NotNullable()
              .WithColumn("level").AsInt16().NotNullable()
              .WithColumn("eventid").AsString(255).Nullable()
              .WithColumn("message").AsString(500).Nullable()
              .WithColumn("exception").AsString(500).Nullable()
              .WithColumn("context").AsString(500).Nullable()
              .WithColumn("state").AsString(500).Nullable()
              .WithColumn("threadid").AsString(50).Nullable()
              .WithColumn("createtime").AsDateTime().WithDefaultValue(DateTime.Now);
        }
    }
}
