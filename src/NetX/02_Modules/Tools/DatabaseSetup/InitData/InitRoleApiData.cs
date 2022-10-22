using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tools.DatabaseSetup.InitData
{
    /// <summary>
    /// 
    /// </summary>
    [Migration(2204)]
    public class InitRoleApiData : InitDataMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public InitRoleApiData()
            : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLEAPI)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Down()
        {
            // Execute.Sql($"delete * from {_tableName}");
        }

        public override void Up()
        {
            Insert.IntoTable(_tableName)
                .Row(new
            {
                roleid = "00000000000000000000000000000001",
                apiid = "60000000000000000000000000000000",
            })
                .Row(new
            {
                roleid = "00000000000000000000000000000001",
                apiid = "60000000000000000000000000000001",
            })
                ;

        }
    }
}
