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
    [Migration(20091127100602)]
    public class InitRoleMenuData : InitDataMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public InitRoleMenuData()
            : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLEMENU)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Up()
        {
            Insert.IntoTable(_tableName)
                .Row(new
                {
                    roleid = "00000000000000000000000000000001",
                    menuid = "00000000000000000000000000000009"
                })
                .Row(new
                {
                    roleid = "00000000000000000000000000000001",
                    menuid = "00000000000000000000000000000010"
                })
                .Row(new
                {
                    roleid = "00000000000000000000000000000001",
                    menuid = "00000000000000000000000000000011"
                })
                .Row(new
                {
                    roleid = "00000000000000000000000000000001",
                    menuid = "00000000000000000000000000000012"
                });
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Down()
        {
            Execute.Sql($"delete from {_tableName} where menuid in ('00000000000000000000000000000009','00000000000000000000000000000010','00000000000000000000000000000011','00000000000000000000000000000012')");
        }
    }
}
