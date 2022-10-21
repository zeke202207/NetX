using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tools.DatabaseSetup.CreateTable
{
    /// <summary>
    /// 系统审计日志表
    /// </summary>
    [Migration(2002)]
    public class AuditLoggingTable : CreateTableMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public AuditLoggingTable()
            : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_AUDIT_LOGGING)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Up()
        {
            Create.Table(_tableName)
              .WithColumn("id").AsString(50).PrimaryKey()
              .WithColumn("userid").AsString(50).Nullable()
              .WithColumn("username").AsString(255).Nullable()
              .WithColumn("controller").AsString(50).Nullable()
              .WithColumn("action").AsString(50).Nullable()
              .WithColumn("remoteipv4").AsString(50).Nullable()
              .WithColumn("httpmethod").AsString(10).Nullable()
              .WithColumn("detail").AsCustom("mediumtext").Nullable()
              .WithColumn("createtime").AsDateTime().WithDefaultValue(DateTime.Now);
        }
    }
}
