using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.DatabaseSetup.InitData
{
    [Migration(20230619150202)]
    public class InitSchedulMenuData : InitDataMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public InitSchedulMenuData()
            : base("sys_menu")
        {
        }


        public override void Up()
        {
            //菜单
            Insert.IntoTable(_tableName)
             .Row(new
             {
                 id = "00000000000000000000000000000015",
                 parentid = "00000000000000000000000000000000",
                 title = "任务调度",
                 path = "/schedul",
                 component = "LAYOUT",
                 redirect = "",
                 meta = "{\"Title\":\"任务调度\",\"HideBreadcrumb\":false,\"Icon\":\"ant-design:account-book-outlined\"}",
                 icon = "ant-design:account-book-outlined",
                 type = 0,// (int)MenuType.Dir,
                 permission = "",
                 orderno = 2,
             });
        }

        public override void Down()
        {
            Execute.Sql($"delete from {_tableName} WHERE id ='00000000000000000000000000000015'");
        }
    }
}
