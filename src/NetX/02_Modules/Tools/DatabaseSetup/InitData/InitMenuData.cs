using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.Tools.DatabaseSetup.InitData
{
    /// <summary>
    /// 
    /// </summary>
    [Migration(2201)]
    public class InitMenuData : InitDataMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public InitMenuData()
            : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSMENU)
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
                    id = "00000000000000000000000000000009",
                    parentid = "00000000000000000000000000000003",
                    name = "日志管理",
                    path = "logging",
                    component = "/systemmanager/account/index",
                    meta = "{\"HideMenu\":false,\"Title\":\"日志管理\",\"HideBreadcrumb\":true,\"Icon\":\"ant-design:usergroup-add-outlined\"}",
                    icon = "ant-design:usergroup-add-outlined",
                    type = 1,
                    permission = "",
                    orderno = 6,
                });
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Down()
        {
            //Execute.Sql($"delete * from {_tableName}");
        }
    }
}
