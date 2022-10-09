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
    /// 
    /// </summary>
    [Migration(2002)]
    public class AuditLogTable : CreateTableMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public AuditLogTable() 
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
            .WithColumn("nickname").AsString(255).Nullable()
            .WithColumn("operation").AsString(50).Nullable().WithColumnDescription("访问api接口地址")
            .WithColumn("requestparams").AsString(5000).Nullable().WithColumnDescription("请求参数")
            .WithColumn("ip").AsString(50).Nullable().WithDefaultValue("1.1.1.1")
            .WithColumn("response").AsString(5000).Nullable()
            .WithColumn("influencedata").AsString(5000).Nullable()
            .WithColumn("createtime").AsDateTime().WithDefaultValue(DateTime.Now);
        }
    }
}
