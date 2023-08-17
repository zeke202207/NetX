using FluentMigrator;
using NetX.DatabaseSetup;

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
            try
            {
                //菜单
                Insert.IntoTable(_tableName)
                 .Row(new
                 {
                     id = "00000000000000000000000000000015",
                     parentid = "00000000000000000000000000000000",
                     title = "任务调度",
                     path = "/schedul",
                     component = "/schedulmanager/index.vue",
                     redirect = "",
                     meta = "{\"Title\":\"任务调度\",\"HideBreadcrumb\":false,\"Icon\":\"ant-design:account-book-outlined\"}",
                     icon = "ant-design:account-book-outlined",
                     type = 1,// (int)MenuType.Menu,
                     permission = "",
                     orderno = 3,
                     issystem = true,
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
                Execute.Sql($"delete from {_tableName} WHERE id ='00000000000000000000000000000015'");
            }
            catch (Exception)
            {
            }
        }
    }
}
