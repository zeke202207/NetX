using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.DatabaseSetup.InitData
{
    /// <summary>
    /// 
    /// </summary>
    [Migration(20091125100608)]
    public class InitApiData: InitDataMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public InitApiData()
            : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSAPI)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Up()
        {
            Insert.IntoTable(_tableName)

            #region account
                .Row(new
                {
                    id = "10000000000000000000000000000000",
                    path = "/api/account/login",
                    group = "account",
                    method = "POST",
                    description = "登录"
                })
                .Row(new
                {
                    id = "10000000000000000000000000000001",
                    path = "/api/account/getuserinfo",
                    group = "account",
                    method = "GET",
                    description = "获取用户信息"
                })
                .Row(new
                {
                    id = "10000000000000000000000000000002",
                    path = "/api/account/getaccountlist",
                    group = "account",
                    method = "GET",
                    description = "获取用户列表集合"
                })
                .Row(new
                {
                    id = "10000000000000000000000000000003",
                    path = "/api/account/isaccountexist",
                    group = "account",
                    method = "GET",
                    description = "验证用户名是否存在"
                })
                .Row(new
                {
                    id = "10000000000000000000000000000004",
                    path = "/api/account/getpermcode",
                    group = "account",
                    method = "GET",
                    description = "获取用户权限码集合"
                })
                .Row(new
                {
                    id = "10000000000000000000000000000005",
                    path = "/api/account/logout",
                    group = "account",
                    method = "GET",
                    description = "登出系统"
                })
                .Row(new
                {
                    id = "10000000000000000000000000000006",
                    path = "/api/account/addaccount",
                    group = "account",
                    method = "POST",
                    description = "添加新用户"
                })
                .Row(new
                {
                    id = "10000000000000000000000000000007",
                    path = "/api/account/updateaccount",
                    group = "account",
                    method = "POST",
                    description = "更新用户信息"
                })
                .Row(new
                {
                    id = "10000000000000000000000000000008",
                    path= "/api/account/removeaccount",
                    group= "account",
                    method= "DELETE",
                    description="删除用户"
                })
                .Row(new
                {
                    id = "10000000000000000000000000000009",
                    path = "/api/account/changepassword",
                    group = "account",
                    method = "POST",
                    description = "修改登录用户密码"
                })


            #endregion

            #region dept
                .Row(new
                {
                    id = "20000000000000000000000000000000",
                    path = "/api/dept/getdeptlist",
                    group = "department",
                    method = "GET",
                    description = "获取部门列表"
                })
                .Row(new
                {
                    id = "20000000000000000000000000000001",
                    path = "/api/dept/adddept",
                    group = "department",
                    method = "POST",
                    description = "新增部门信息"
                })
                .Row(new
                {
                    id = "20000000000000000000000000000002",
                    path = "/api/dept/updatedept",
                    group = "department",
                    method = "POST",
                    description = "更新部门信息"
                })
                .Row(new
                {
                    id = "20000000000000000000000000000003",
                    path = "/api/dept/removedept",
                    group = "department",
                    method = "DELETE",
                    description = "删除部门"
                })
            #endregion

            #region menu
                .Row(new
                {
                    id = "30000000000000000000000000000000",
                    path = "/api/menu/getcurrentusermenulist",
                    group = "menu",
                    method = "GET",
                    description = "获取登录用户授权的菜单列表"
                })
                .Row(new
                {
                    id = "30000000000000000000000000000001",
                    path = "/api/menu/getmenulist",
                    group = "menu",
                    method = "POST",
                    description = "获取菜单列表"
                })
                .Row(new
                {
                    id = "30000000000000000000000000000002",
                    path = "/api/menu/addmenu",
                    group = "menu",
                    method = "POST",
                    description = "新增菜单信息"
                })
                .Row(new
                {
                    id = "30000000000000000000000000000003",
                    path = "/api/menu/updatemenu",
                    group = "menu",
                    method = "POST",
                    description = "更新菜单信息"
                })
                .Row(new
                {
                    id = "30000000000000000000000000000004",
                    path = "/api/menu/removemenu",
                    group = "menu",
                    method = "DELETE",
                    description = "删除菜单"
                })
            #endregion

            #region role
                .Row(new
                {
                    id = "40000000000000000000000000000000",
                    path = "/api/role/getrolelistbypage",
                    group = "role",
                    method = "POST",
                    description = "获取角色列表"
                })
                .Row(new
                {
                    id = "40000000000000000000000000000001",
                    path = "/api/role/getallrolelist",
                    group = "role",
                    method = "GET",
                    description = "获取全部角色列表"
                })
                .Row(new
                {
                    id = "40000000000000000000000000000002",
                    path = "/api/role/addrole",
                    group = "role",
                    method = "POST",
                    description = "添加新角色信息"
                })
                .Row(new
                {
                    id = "40000000000000000000000000000003",
                    path = "/api/role/updaterole",
                    group = "role",
                    method = "POST",
                    description = "更新角色信息"
                })
                .Row(new
                {
                    id = "40000000000000000000000000000004",
                    path = "/api/role/removerole",
                    group = "role",
                    method = "DELETE",
                    description = "删除角色"
                })
                .Row(new
                {
                    id = "40000000000000000000000000000005",
                    path = "/api/role/setrolestatus",
                    group = "role",
                    method = "POST",
                    description = "更新角色状态"
                })
                .Row(new
                {
                    id = "40000000000000000000000000000006",
                    path = "/api/role/setapiauthstatus",
                    group = "role",
                    method = "POST",
                    description = "更新角色后台鉴权状态"
                })
                .Row(new
                {
                    id = "40000000000000000000000000000007",
                    path = "/api/role/getapiauth",
                    group = "role",
                    method = "POST",
                    description = "获取api鉴权id集合"
                })
                .Row(new
                {
                    id = "40000000000000000000000000000008",
                    path = "/api/role/setapiauth",
                    group = "role",
                    method = "POST",
                    description = "设置api鉴权id集合"
                })

            #endregion

            #region api

            .Row(new
            {
                id = "50000000000000000000000000000000",
                path = "/api/api/getapipagelist",
                group = "api",
                method = "POST",
                description = "获取接口分页列表"
            })
            .Row(new
            {
                id = "50000000000000000000000000000001",
                path = "/api/api/getapilist",
                group = "api",
                method = "POST",
                description = "获取全部接口列表"
            })
            .Row(new
            {
                id = "50000000000000000000000000000002",
                path = "/api/api/addapi",
                group = "api",
                method = "POST",
                description = "添加新的接口"
            })
            .Row(new
            {
                id = "50000000000000000000000000000003",
                path = "/api/api/updateapi",
                group = "api",
                method = "POST",
                description = "更新接口信息"
            })
            .Row(new
            {
                id = "50000000000000000000000000000004",
                path = "/api/api/removeapi",
                group = "api",
                method = "DELETE",
                description = "删除接口"
            })

            #endregion
                ;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Down()
        {
            Execute.Sql($"delete from {_tableName}");
        }
    }
}
