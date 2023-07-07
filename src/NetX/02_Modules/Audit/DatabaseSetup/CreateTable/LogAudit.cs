using FluentMigrator;
using NetX.Audit.Models.Entity;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Audit.DatabaseSetup
{
    [Migration(20230628104108)]
    public class LogAudit : CreateTableMigration
    {
        public LogAudit() 
            : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_AUDIT)
        {
        }

        public override void Up()
        {
            try
            {
                Create.Table(_tableName)
                       .WithColumn(nameof(sys_log_audit.Id).ToLower()).AsString(50).PrimaryKey()
                       .WithColumn(nameof(sys_log_audit.parameters).ToLower()).AsCustom("TEXT").Nullable()
                       .WithColumn(nameof(sys_log_audit.browserinfo).ToLower()).AsString(500).Nullable()
                       .WithColumn(nameof(sys_log_audit.clientname).ToLower()).AsString(500).Nullable()
                       .WithColumn(nameof(sys_log_audit.clientipaddress).ToLower()).AsString().Nullable()
                       .WithColumn(nameof(sys_log_audit.executionduration).ToLower()).AsInt32().Nullable()
                       .WithColumn(nameof(sys_log_audit.executiontime).ToLower()).AsDateTime2().Nullable()
                       .WithColumn(nameof(sys_log_audit.returnvalue).ToLower()).AsCustom("TEXT").Nullable()
                       .WithColumn(nameof(sys_log_audit.exception).ToLower()).AsCustom("TEXT").Nullable()
                       .WithColumn(nameof(sys_log_audit.methodname).ToLower()).AsString(500).Nullable()
                       .WithColumn(nameof(sys_log_audit.servicename).ToLower()).AsString(500).Nullable()
                       .WithColumn(nameof(sys_log_audit.userid).ToLower()).AsString(50).Nullable()
                       .WithColumn(nameof(sys_log_audit.customdata).ToLower()).AsCustom("TEXT").Nullable()
                       .WithColumn(nameof(sys_log_audit.desc).ToLower()).AsCustom("TEXT").Nullable();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
