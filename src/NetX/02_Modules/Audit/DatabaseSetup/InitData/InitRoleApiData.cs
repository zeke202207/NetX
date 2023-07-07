using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Audit.DatabaseSetup.InitData
{
    [Migration(20230705093702)]
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
        /// <exception cref="NotImplementedException"></exception>
        public override void Up()
        {
            try
            {
                Insert.IntoTable(_tableName)
                .Row(new
                {
                    roleid = "00000000000000000000000000000001",
                    apiid = "70000000000000000000000000000000",
                });
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Down()
        {
            try
            {
                Execute.Sql($"DELETE FROM {_tableName} WHERE " +
                    $"( roleid = '00000000000000000000000000000001' and apiid ='70000000000000000000000000000000')");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
