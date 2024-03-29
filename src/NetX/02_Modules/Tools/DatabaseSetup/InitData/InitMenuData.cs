﻿using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.Tools.DatabaseSetup.InitData
{
    /// <summary>
    /// 
    /// </summary>
    [Migration(20091127100601)]
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
                    title = "日志管理",
                    path = "/log",
                    component = "/LAYOUT",
                    redirect = "",
                    type = 0,
                    meta = "{\"Title\":\"日志管理\",\"Icon\":\"ant-design:book-outlined\"}",
                    icon = "ant-design:book-outlined",
                    permission = "",
                    orderno = 6,
                })
                .Row(new
                {
                    id = "00000000000000000000000000000010",
                    parentid = "00000000000000000000000000000009",
                    title = "系统日志",
                    path = "syslogging",
                    component = "/logmanager/syslogging/index",
                    meta = "{\"ignoreKeepAlive\":false,\"currentActiveMenu\":null,\"KeepAlive\":true,\"HideMenu\":false,\"Title\":\"系统日志\",\"HideBreadcrumb\":true,\"Icon\":\"ant-design:snippets-twotone\"}",
                    icon = "ant-design:snippets-twotone",
                    type = 1,
                    permission = "",
                    orderno = 1,
                })
                .Row(new
                {
                    id = "00000000000000000000000000000011",
                    parentid = "00000000000000000000000000000009",
                    title = "审计日志",
                    path = "auditlogging",
                    component = "/logmanager/auditlogging/index",
                    meta = "{\"ignoreKeepAlive\":false,\"currentActiveMenu\":null,\"KeepAlive\":true,\"HideMenu\":false,\"Title\":\"审计日志\",\"HideBreadcrumb\":true,\"Icon\":\"ant-design:solution-outlined\"}",
                    icon = "ant-design:solution-outlined",
                    type = 1,
                    permission = "",
                    orderno = 2,
                })
                .Row(new
                {
                    id = "00000000000000000000000000000012",
                    parentid = "00000000000000000000000000000009",
                    title = "登录日志",
                    path = "loginlogging",
                    component = "/logmanager/loginlogging/index",
                    meta = "{\"ignoreKeepAlive\":false,\"currentActiveMenu\":null,\"KeepAlive\":true,\"HideMenu\":false,\"Title\":\"登录日志\",\"HideBreadcrumb\":true,\"Icon\":\"ant-design:solution-outlined\"}",
                    icon = "ant-design:file-twotone",
                    type = 1,
                    permission = "",
                    orderno = 3,
                });
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Down()
        {
            Execute.Sql($"delete from {_tableName} where id in ('00000000000000000000000000000009','00000000000000000000000000000010','00000000000000000000000000000011','00000000000000000000000000000012')");
        }
    }
}
