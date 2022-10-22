using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tools.DatabaseSetup.InitData
{
    [Migration(2203)]
    public class InitApiData : InitDataMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public InitApiData()
            : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSAPI)
        {
        }
        public override void Up()
        {
            Insert.IntoTable(_tableName)
                 .Row(new
                 {
                     id = "60000000000000000000000000000000",
                     path = "/api/logging/getloglist",
                     group = "log",
                     method = "post",
                     description = "获取系统日志列表"
                 })
                 .Row(new
                 {
                     id = "60000000000000000000000000000001",
                     path = "/api/logging/getauditloglist",
                     group = "log",
                     method = "post",
                     description = "获取审计日志列表"
                 })
                 ;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Down()
        {
            //  Execute.Sql($"delete * from {_tableName}");
        }
    }
}
