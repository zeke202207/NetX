using FluentMigrator;
using NetX.DatabaseSetup;
using NetX.SystemManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.DatabaseSetup.InitData;

[Migration(1002)]
public class InitMenuData : InitDataMigration
{
    public InitMenuData()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSMENU)
    {
    }

    public override void Up()
    {
        Insert.IntoTable(_tableName)
            .Row(new
            {
                id = "00000000000000000000000000000001",
                parentid = "00000000000000000000000000000000",
                name = "仪表盘",
                path = "workbench",
                component = "/dashboard/workbench/index",
                redirect = "",
                meta = "{\"Title\":\"仪表盘\",\"HideBreadcrumb\":true,\"Icon\":\"bx:bx-home\"}",
                icon = "ion:layers-outline",
                type= (int)MenuType.Menu,
                permission="",
                orderno=1,
            })
            .Row(new
            {
                id = "00000000000000000000000000000003",
                parentid = "00000000000000000000000000000000",
                name = "系统管理",
                path = "/system",
                component = "LAYOUT",
                redirect = "",
                meta = "{\"Title\":\"系统管理\",\"HideBreadcrumb\":false,\"Icon\":\"ant-design:setting-outlined\"}",
                icon = "ant-design:setting-outlined",
                type = (int)MenuType.Dir,
                permission = "",
                orderno = 2,
            })
            .Row(new
            {
                id = "00000000000000000000000000000004",
                parentid = "00000000000000000000000000000003",
                name = "账号管理",
                path = "account",
                component = "/systemmanager/account/index",
                meta = "{\"HideMenu\":false,\"Title\":\"账号管理\",\"HideBreadcrumb\":true,\"Icon\":\"ant-design:usergroup-add-outlined\"}",
                icon = "ant-design:usergroup-add-outlined",
                type = (int)MenuType.Menu,
                permission = "",
                orderno = 1,
            })
            .Row(new
            {
                id = "00000000000000000000000000000005",
                parentid = "00000000000000000000000000000003",
                name = "菜单管理",
                path = "menu",
                component = "/systemmanager/menu/index",
                meta = "{\"HideMenu\":false,\"Title\":\"菜单管理\",\"HideBreadcrumb\":true,\"Icon\":\"ant-design:menu-outlined\"}",
                icon = "ant-design:menu-outlined",
                type = (int)MenuType.Menu,
                permission = "",
                orderno = 2,
            })
            .Row(new
            {
                id = "00000000000000000000000000000006",
                parentid = "00000000000000000000000000000003",
                name = "角色管理",
                path = "role",
                component = "/systemmanager/role/index",
                meta = "{\"HideMenu\":false,\"Title\":\"角色管理\",\"HideBreadcrumb\":true,\"Icon\":\"ant-design:security-scan-outlined\"}",
                icon = "ant-design:security-scan-outlined",
                type = (int)MenuType.Menu,
                permission = "",
                orderno = 3,
            })
            .Row(new
            {
                id = "00000000000000000000000000000007",
                parentid = "00000000000000000000000000000003",
                name = "部门管理",
                path = "dept",
                component = "/systemmanager/dept/index",
                meta = "{\"HideMenu\":false,\"Title\":\"部门管理\",\"HideBreadcrumb\":true,\"Icon\":\"ant-design:apartment-outlined\"}",
                icon = "ant-design:apartment-outlined",
                type = (int)MenuType.Menu,
                permission = "",
                orderno = 4,
            });
    }

    public override void Down()
    {
        Execute.Sql($"delete * from {_tableName}");
    }
}
