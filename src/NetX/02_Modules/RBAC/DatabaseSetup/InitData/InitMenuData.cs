using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.RBAC.DatabaseSetup.InitData;

/// <summary>
/// 
/// </summary>
[Migration(20091125100602)]
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
        try
        {
            Insert.IntoTable(_tableName)
                .Row(new
                {
                    id = "00000000000000000000000000000001",
                    parentid = "00000000000000000000000000000000",
                    title = "仪表盘",
                    path = "/dashboard",
                    component = "/dashboard/workbench/index",
                    redirect = "",
                    meta = "{\"affix\":true,\"Title\":\"仪表盘\",\"HideBreadcrumb\":true,\"Icon\":\"bx:bx-home\",\"CurrentActiveMenu\":\"dashboard\"}",
                    icon = "ion:layers-outline",
                    type = (int)MenuType.Menu,
                    permission = "",
                    orderno = 1,
                })
                .Row(new
                {
                    id = "00000000000000000000000000000003",
                    parentid = "00000000000000000000000000000000",
                    title = "系统管理",
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
                    title = "账号管理",
                    path = "Account",
                    component = "/systemmanager/account/index",
                    meta = "{\"ignoreKeepAlive\":false,\"currentActiveMenu\":null,\"KeepAlive\":true,\"HideMenu\":false,\"Title\":\"账号管理\",\"HideBreadcrumb\":true,\"Icon\":\"ant-design:usergroup-add-outlined\"}",
                    icon = "ant-design:usergroup-add-outlined",
                    type = (int)MenuType.Menu,
                    permission = "",
                    orderno = 1,
                })
                .Row(new
                {
                    id = "00000000000000000000000000000005",
                    parentid = "00000000000000000000000000000003",
                    title = "菜单管理",
                    path = "Menu",
                    component = "/systemmanager/menu/index",
                    meta = "{\"ignoreKeepAlive\":false,\"currentActiveMenu\":null,\"KeepAlive\":true,\"HideMenu\":false,\"Title\":\"菜单管理\",\"HideBreadcrumb\":true,\"Icon\":\"ant-design:menu-outlined\"}",
                    icon = "ant-design:menu-outlined",
                    type = (int)MenuType.Menu,
                    permission = "",
                    orderno = 2,
                })
                .Row(new
                {
                    id = "00000000000000000000000000000006",
                    parentid = "00000000000000000000000000000003",
                    title = "角色管理",
                    path = "Role",
                    component = "/systemmanager/role/index",
                    meta = "{\"ignoreKeepAlive\":false,\"currentActiveMenu\":null,\"KeepAlive\":true,\"HideMenu\":false,\"Title\":\"角色管理\",\"HideBreadcrumb\":true,\"Icon\":\"ant-design:security-scan-outlined\"}",
                    icon = "ant-design:security-scan-outlined",
                    type = (int)MenuType.Menu,
                    permission = "",
                    orderno = 3,
                })
                .Row(new
                {
                    id = "00000000000000000000000000000007",
                    parentid = "00000000000000000000000000000003",
                    title = "部门管理",
                    path = "Dept",
                    component = "/systemmanager/dept/index",
                    meta = "{\"ignoreKeepAlive\":false,\"currentActiveMenu\":null,\"KeepAlive\":true,\"HideMenu\":false,\"Title\":\"部门管理\",\"HideBreadcrumb\":true,\"Icon\":\"ant-design:apartment-outlined\"}",
                    icon = "ant-design:apartment-outlined",
                    type = (int)MenuType.Menu,
                    permission = "",
                    orderno = 4,
                })
                .Row(new
                {
                    id = "00000000000000000000000000000008",
                    parentid = "00000000000000000000000000000003",
                    title = "接口管理",
                    path = "Apicontract",
                    component = "/systemmanager/apicontract/index",
                    meta = "{\"ignoreKeepAlive\":false,\"currentActiveMenu\":null,\"KeepAlive\":true,\"HideMenu\":false,\"Title\":\"接口管理\",\"HideBreadcrumb\":true,\"Icon\":\"ant-design:api-outlined\"}",
                    icon = "ant-design:api-outlined",
                    type = (int)MenuType.Menu,
                    permission = "",
                    orderno = 5,
                })
                .Row(new
                {
                    id = "00000000000000000000000000000013",
                    parentid = "00000000000000000000000000000003",
                    title = "个人设置",
                    path = "Setting",
                    component = "/systemmanager/account/setting/index",
                    meta = "{\"ignoreKeepAlive\":false,\"currentActiveMenu\":null,\"KeepAlive\":true,\"HideMenu\":true,\"Title\":\"个人设置\",\"HideBreadcrumb\":true,\"Icon\":\"ant-design:user-outlined\"}",
                    icon = "ant-design:user-outlined",
                    type = (int)MenuType.Menu,
                    permission = "",
                    orderno = 6,
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
            Execute.Sql($"delete from {_tableName}");
        }
        catch (Exception ex)
        {
        }
    }
}
