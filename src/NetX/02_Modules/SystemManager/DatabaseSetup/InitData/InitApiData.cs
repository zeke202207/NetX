﻿using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.DatabaseSetup.InitData
{
    /// <summary>
    /// 
    /// </summary>
    [Migration(1008)]
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
                    method = "post",
                    description = "登录"
                })
                .Row(new
                {
                    id = "10000000000000000000000000000001",
                    path = "/api/account/getuserinfo",
                    group = "account",
                    method = "get",
                    description = "获取用户信息"
                })
                .Row(new
                {
                    id = "10000000000000000000000000000002",
                    path = "/api/account/getaccountlist",
                    group = "account",
                    method = "get",
                    description = "获取用户列表集合"
                })
                .Row(new
                {
                    id = "10000000000000000000000000000003",
                    path = "/api/account/isaccountexist",
                    group = "account",
                    method = "get",
                    description = "验证用户名是否存在"
                })
                .Row(new
                {
                    id = "10000000000000000000000000000004",
                    path = "/api/account/getpermcode",
                    group = "account",
                    method = "get",
                    description = "获取用户权限码集合"
                })
                .Row(new
                {
                    id = "10000000000000000000000000000005",
                    path = "/api/account/logout",
                    group = "account",
                    method = "get",
                    description = "登出系统"
                })
                .Row(new
                {
                    id = "10000000000000000000000000000006",
                    path = "/api/account/addaccount",
                    group = "account",
                    method = "post",
                    description = "添加新用户"
                })
                .Row(new
                {
                    id = "10000000000000000000000000000007",
                    path = "/api/account/updateaccount",
                    group = "account",
                    method = "post",
                    description = "更新用户信息"
                })
                .Row(new
                {
                    id = "10000000000000000000000000000008",
                    path= "/api/account/removeaccount",
                    group= "account",
                    method= "delete",
                    description="删除用户"
                })
            #endregion

            #region dept
                .Row(new
                {
                    id = "20000000000000000000000000000000",
                    path = "/api/dept/getdeptlist",
                    group = "department",
                    method = "get",
                    description = "获取部门列表"
                })
                .Row(new
                {
                    id = "20000000000000000000000000000001",
                    path = "/api/dept/adddept",
                    group = "department",
                    method = "post",
                    description = "新增部门信息"
                })
                .Row(new
                {
                    id = "20000000000000000000000000000002",
                    path = "/api/dept/updatedept",
                    group = "department",
                    method = "post",
                    description = "更新部门信息"
                })
                .Row(new
                {
                    id = "20000000000000000000000000000003",
                    path = "/api/dept/removedept",
                    group = "department",
                    method = "delete",
                    description = "删除部门"
                })
            #endregion

            #region menu
                .Row(new
                {
                    id = "30000000000000000000000000000000",
                    path = "/api/menu/getcurrentusermenulist",
                    group = "menu",
                    method = "get",
                    description = "获取登录用户授权的菜单列表"
                })
                .Row(new
                {
                    id = "30000000000000000000000000000001",
                    path = "/api/menu/getmenulist",
                    group = "menu",
                    method = "post",
                    description = "获取菜单列表"
                })
                .Row(new
                {
                    id = "30000000000000000000000000000002",
                    path = "/api/menu/addmenu",
                    group = "menu",
                    method = "post",
                    description = "新增菜单信息"
                })
                .Row(new
                {
                    id = "30000000000000000000000000000003",
                    path = "/api/menu/updatemenu",
                    group = "menu",
                    method = "post",
                    description = "更新菜单信息"
                })
                .Row(new
                {
                    id = "30000000000000000000000000000004",
                    path = "/api/menu/removemenu",
                    group = "menu",
                    method = "delete",
                    description = "删除菜单"
                })
            #endregion

            #region role
                .Row(new
                {
                    id = "40000000000000000000000000000000",
                    path = "/api/role/getrolelistbypage",
                    group = "role",
                    method = "post",
                    description = "获取角色列表"
                })
                .Row(new
                {
                    id = "40000000000000000000000000000001",
                    path = "/api/role/getallrolelist",
                    group = "role",
                    method = "get",
                    description = "获取全部角色列表"
                })
                .Row(new
                {
                    id = "40000000000000000000000000000002",
                    path = "/api/role/addrole",
                    group = "role",
                    method = "post",
                    description = "添加新角色信息"
                })
                .Row(new
                {
                    id = "40000000000000000000000000000003",
                    path = "/api/role/updaterole",
                    group = "role",
                    method = "post",
                    description = "更新角色信息"
                })
                .Row(new
                {
                    id = "40000000000000000000000000000004",
                    path = "/api/role/removerole",
                    group = "role",
                    method = "delete",
                    description = "删除角色"
                })
                .Row(new
                {
                    id = "40000000000000000000000000000005",
                    path = "/api/role/setrolestatus",
                    group = "role",
                    method = "post",
                    description = "更新角色状态"
                })

            #endregion

            #region api


            #endregion
                ;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Down()
        {
            Execute.Sql($"delete * from {_tableName}");
        }
    }
}