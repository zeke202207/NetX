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
                name = "Dashboard",
                path = "/dashboard",
                component = "LAYOUT",
                redirect = "/dashboard/analysis",
                meta = "{\"Title\":\"routes.dashboard.dashboard\",\"HideBreadcrumb\":true,\"Icon\":\"bx:bx-home\"}",
                icon = "ion:layers-outline",
                type= (int)MenuType.Directory,
                permission="",
                orderno=1,
            })
            .Row(new
            {
                id = "00000000000000000000000000000002",
                parentid = "00000000000000000000000000000001",
                name = "Analysis",
                path = "analysis",
                component = "/dashboard/analysis/index",
                meta = "{\"HideMenu\":false,\"Title\":\"routes.dashboard.analysis\",\"HideBreadcrumb\":true,\"Icon\":\"bx:bx-home\"}",
                icon = "ion:document",
                type = (int)MenuType.Menu,
                permission = "menu1:view",
                orderno = 1,
            })
            .Row(new
            {
                id = "00000000000000000000000000000003",
                parentid = "00000000000000000000000000000000",
                name = "System",
                path = "/system",
                component = "LAYOUT",
                redirect = "/dashboard/account",
                meta = "{\"Title\":\"routes.demo.system.moduleName\",\"HideBreadcrumb\":false,\"Icon\":\"ion:settings-outline\"}",
                icon = "ion:git-compare-outline",
                type = (int)MenuType.Directory,
                permission = "",
                orderno = 2,
            })
            .Row(new
            {
                id = "00000000000000000000000000000004",
                parentid = "00000000000000000000000000000003",
                name = "AccountManagement",
                path = "account",
                component = "/systemmanager/account/index",
                meta = "{\"HideMenu\":false,\"Title\":\"routes.demo.system.account\",\"HideBreadcrumb\":true,\"Icon\":\"bx:bx-home\"}",
                icon = "ion:document",
                type = (int)MenuType.Menu,
                permission = "menu1:view",
                orderno = 1,
            })
            .Row(new
            {
                id = "00000000000000000000000000000005",
                parentid = "00000000000000000000000000000003",
                name = "MenuManagement",
                path = "menu",
                component = "/systemmanager/menu/index",
                meta = "{\"HideMenu\":false,\"Title\":\"routes.demo.system.menu\",\"HideBreadcrumb\":true,\"Icon\":\"bx:bx-home\"}",
                icon = "ion:document",
                type = (int)MenuType.Menu,
                permission = "menu1:view",
                orderno = 2,
            })
            .Row(new
            {
                id = "00000000000000000000000000000006",
                parentid = "00000000000000000000000000000003",
                name = "RoleManagement",
                path = "role",
                component = "/systemmanager/role/index",
                meta = "{\"HideMenu\":false,\"Title\":\"routes.demo.system.role\",\"HideBreadcrumb\":true,\"Icon\":\"bx:bx-home\"}",
                icon = "ion:document",
                type = (int)MenuType.Menu,
                permission = "menu1:view",
                orderno = 3,
            })
            .Row(new
            {
                id = "00000000000000000000000000000007",
                parentid = "00000000000000000000000000000003",
                name = "DeptManagement",
                path = "dept",
                component = "/systemmanager/dept/index",
                meta = "{\"HideMenu\":false,\"Title\":\"routes.demo.system.dept\",\"HideBreadcrumb\":true,\"Icon\":\"bx:bx-home\"}",
                icon = "ion:document",
                type = (int)MenuType.Menu,
                permission = "menu1:view",
                orderno = 4,
            });
    }

    public override void Down()
    {
        Execute.Sql($"delete * from {_tableName}");
    }
}
