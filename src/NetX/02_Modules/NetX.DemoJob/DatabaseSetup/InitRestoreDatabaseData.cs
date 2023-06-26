using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.DemoJob.DatabaseSetup
{
    [Migration(20230626134902)]
    public class InitRestoreDatabaseData : InitDataMigration
    {
        private readonly string _jobTable = "sys_jobtask";
        private readonly string _triggerTable = "sys_jobtasktrigger";

        public InitRestoreDatabaseData() : base("test")
        {
        }

        public override void Up()
        {
            try
            {
                Insert.IntoTable(_jobTable)
                   .Row(new
                   {
                       id = "cd88e637977b417382011fc040cd4da4",
                       name = "restoredatabase",
                       group = "admintool",
                       jobtype = "1",
                       datamap = "{\"tenantid\":\"1\"}", //租戶id默認給配置文件第一個
                       disallowconcurrentexecution = 1,
                       enabled = 1,
                       state = 1,
                       createtime = DateTime.Now,
                       description = "数据库定时还原"
                   });
                Insert.IntoTable(_triggerTable)
                   .Row(new
                   {
                       id = "73409ef560414618923a49ece2aacb8b",
                       jobtaskid = "cd88e637977b417382011fc040cd4da4",
                       name = "restoredatabase trigger",
                       cron = "0 0/10 * * * ?", //每10分钟执行一次
                       triggertype = 0,
                       startnow = 1,
                       priority = 1,
                       createtime = DateTime.Now,
                       description = "数据库定时还原触发器"
                   });
            }
            catch (Exception ex)
            {
            }
        }

        public override void Down()
        {
            try
            {
                Execute.Sql($"delete from {_jobTable}");
                Execute.Sql($"delete from {_triggerTable}");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
