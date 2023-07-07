using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.DatabaseSetup.InitData
{
    /// <summary>
    /// 
    /// </summary>
    [Migration(20230628131302)]
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
                    apiid = "60000000000000000000000000000000",
                })
                .Row(new
                {
                    roleid = "00000000000000000000000000000001",
                    apiid = "60000000000000000000000000000001",
                })
                .Row(new
                {
                    roleid = "00000000000000000000000000000001",
                    apiid = "60000000000000000000000000000002",
                })
                .Row(new
                {
                    roleid = "00000000000000000000000000000001",
                    apiid = "60000000000000000000000000000003",
                })
                .Row(new
                {
                    roleid = "00000000000000000000000000000001",
                    apiid = "60000000000000000000000000000004",
                })
                .Row(new
                {
                    roleid = "00000000000000000000000000000001",
                    apiid = "60000000000000000000000000000005",
                })
                .Row(new
                {
                    roleid = "00000000000000000000000000000001",
                    apiid = "60000000000000000000000000000006",
                })
                .Row(new
                {
                    roleid = "00000000000000000000000000000001",
                    apiid = "60000000000000000000000000000007",
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
                    $"( roleid = '00000000000000000000000000000001' and apiid ='60000000000000000000000000000000') ||  " +
                    $"( roleid = '00000000000000000000000000000001' and apiid ='60000000000000000000000000000001')||  " +
                    $"( roleid = '00000000000000000000000000000001' and apiid ='60000000000000000000000000000002')||  " +
                    $"( roleid = '00000000000000000000000000000001' and apiid ='60000000000000000000000000000003')||  " +
                    $"( roleid = '00000000000000000000000000000001' and apiid ='60000000000000000000000000000004')||  " +
                    $"( roleid = '00000000000000000000000000000001' and apiid ='60000000000000000000000000000005')||  " +
                    $"( roleid = '00000000000000000000000000000001' and apiid ='60000000000000000000000000000006')||  " +
                    $"( roleid = '00000000000000000000000000000001' and apiid ='60000000000000000000000000000007')");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
