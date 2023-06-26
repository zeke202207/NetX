﻿using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.DatabaseSetup.InitData
{
    [Migration(20230619151002)]
    public class InitSchedulRoleMenuData : InitDataMigration
    {
        public InitSchedulRoleMenuData() : base("sys_role_menu")
        {
        }

        public override void Up()
        {
            try
            {
                Insert.IntoTable(_tableName)
               .Row(new
               {
                   roleid = "00000000000000000000000000000001",
                   menuid = "00000000000000000000000000000015"
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
                Execute.Sql($"delete from {_tableName} WHERE menuid ='00000000000000000000000000000015'");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
