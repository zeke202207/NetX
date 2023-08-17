using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.Caching.DatabaseSetup
{
    [Migration(20230706091802)]
    public class InitCachingMenuData : InitDataMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public InitCachingMenuData()
            : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSMENU)
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
                     id = "00000000000000000000000000000016",
                     parentid = "00000000000000000000000000000000",
                     title = "系统监控",
                     path = "/monitor",
                     component = "LAYOUT",
                     redirect = "",
                     meta = "{\"Title\":\"系统监控\",\"HideBreadcrumb\":false,\"Icon\":\"ant-design:fund-projection-screen-outlined\"}",
                     icon = "ant-design:fund-projection-screen-outlined",
                     type = 0,// (int)MenuType.Dir,
                     permission = "",
                     orderno = 4,
                     issystem = true,
                 })
                 .Row(new
                 {
                     id = "00000000000000000000000000000017",
                     parentid = "00000000000000000000000000000016",
                     title = "缓存列表",
                     path = "caching",
                     component = "/systemmonitor/caching/index",
                     meta = "{\"ignoreKeepAlive\":false,\"currentActiveMenu\":null,\"KeepAlive\":true,\"HideMenu\":false,\"Title\":\"缓存列表\",\"HideBreadcrumb\":true,\"Icon\":\"ant-design:database-outlined\"}",
                     icon = "ant-design:database-outlined",
                     type = 1,//(int)MenuType.Menu,
                     permission = "",
                     orderno = 1,
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
                Execute.Sql($"delete from {_tableName} WHERE id ='00000000000000000000000000000016'");
                Execute.Sql($"delete from {_tableName} WHERE id ='00000000000000000000000000000017'");
            }
            catch (Exception)
            {
            }
        }
    }
}
