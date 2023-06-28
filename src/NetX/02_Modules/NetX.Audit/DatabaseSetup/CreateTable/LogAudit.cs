using FluentMigrator;
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
                       .WithColumn("id").AsString(50).PrimaryKey()
                       .WithColumn("parameters").AsCustom("TEXT").Nullable()
                       .WithColumn("browserinfo").AsString(500).Nullable()
                       .WithColumn("clientname").AsString(500).Nullable()
                       .WithColumn("clientipaddress").AsString().Nullable()
                       .WithColumn("executionduration").AsInt32().Nullable()
                       .WithColumn("executiontime").AsDate().Nullable()
                       .WithColumn("returnvalue").AsCustom("TEXT").Nullable()
                       .WithColumn("exception").AsCustom("TEXT").Nullable()
                       .WithColumn("methodname").AsString(500).Nullable()
                       .WithColumn("servicename").AsString(500).Nullable()
                       .WithColumn("userid").AsString(50).Nullable()
                       .WithColumn("customdata").AsCustom("TEXT").Nullable()
                       .WithColumn("desc").AsCustom("TEXT").Nullable();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
