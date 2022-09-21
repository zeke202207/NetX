using FreeSql;
using Microsoft.AspNetCore.Components;
using NetX.Common.Attributes;
using NetX.SystemManager.Data.Repositories;
using NetX.SystemManager.Models;
using NetX.SystemManager.Models.Dtos.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NetX.SystemManager.Core.Impl
{
    [Scoped]
    public class MenuService :BaseService, IMenuService
    {
        private readonly IBaseRepository<sys_menu> _menuRepository;

        public MenuService(
            IBaseRepository<sys_menu> menuRepository)
        {
            this._menuRepository = menuRepository;
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddMenu(MenuRequestModel model)
        {
            var menuType = GetMenuType(int.Parse(model.Type));
            var menuEntity = new sys_menu()
            {
                id = base.CreateId(),
                createtime = base.CreateInsertTime(),
                parentid = string.IsNullOrWhiteSpace(model.ParentId) ? SystemManagerConst.C_ROOT_ID : model.ParentId,
                status = int.Parse(model.Status),
                component = (Ext)model.IsExt == Ext.Yes ?"IFrame": menuType == MenuType.Dir ? "LAYOUT" : model.Component,
                icon = model.Icon,
                isext = model.IsExt,
                keepalive = model.KeepAlive,
                meta = Newtonsoft.Json.JsonConvert.SerializeObject(new MenuMetaData()
                {
                    Title = model.Name,
                    HideChildrenMenu = false,
                    HideBreadcrumb = (Status)model.Show == Status.Disabled ? true : false,
                    HideMenu = (Status)model.Show == Status.Disabled ? true : false,
                    Icon = model.Icon,
                    IgnoreKeepAlive = (Status)model.Show == Status.Enable ? true : false,
                    FrameSrc = (Ext)model.IsExt == Ext.Yes ? model.ExtPath : "",
                }),
                name = model.Name,
                orderno = (int)model.OrderNo,
                path = menuType == MenuType.Dir ? model.Path.StartsWith("/")? model.Path:$"/{model.Path}":model.Path,
                permission = model.Permission,
                redirect = model.Redirect,
                show = model.Show,
                type = Convert.ToInt32(model.Type)
            };
            await this._menuRepository.InsertAsync(menuEntity);
            return true;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveMenu(string menuId)
        {
            //1. find all children
            var ids = await this._menuRepository.Select
                .WithSql($"select id from sys_menu where find_in_set(id,get_child_menu('{menuId}'))")
                .ToListAsync<string>("id");
            //2. delete all
            return await ((SysMenuRepository)this._menuRepository).RemoveMenu(ids);
        }

        /// <summary>
        /// 跟新菜单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateMenu(MenuRequestModel model)
        {
            var menuType = GetMenuType(int.Parse(model.Type));
            var entity = await this._menuRepository.Select.Where(p => p.id.Equals(model.Id)).FirstAsync();
            entity.parentid = string.IsNullOrWhiteSpace(model.ParentId) ? SystemManagerConst.C_ROOT_ID : model.ParentId;
            entity.status = int.Parse(model.Status);
            entity.component = (Ext)model.IsExt == Ext.Yes ? "IFrame" : menuType == MenuType.Dir ? "LAYOUT" : model.Component;
            entity.icon = model.Icon;
            entity.isext = model.IsExt;
            entity.keepalive = model.KeepAlive;
            entity.meta = Newtonsoft.Json.JsonConvert.SerializeObject(new MenuMetaData()
            {
                Title = model.Name,
                //CurrentActiveMenu = model.Path,
                HideChildrenMenu = false,
                HideBreadcrumb = (Status)model.Show == Status.Disabled ? true : false,
                HideMenu = (Status)model.Show == Status.Disabled ? true : false,
                Icon = model.Icon,
                IgnoreKeepAlive = (Status)model.Show == Status.Enable ? true : false,
                FrameSrc = (Ext)model.IsExt == Ext.Yes ? model.ExtPath : "",
            });
            entity.name = model.Name;
            entity.orderno = (int)model.OrderNo;
            entity.path = menuType == MenuType.Dir ? model.Path.StartsWith("/") ? model.Path : $"/{model.Path}" : model.Path;
            entity.permission = model.Permission;
            entity.redirect = model.Redirect;
            entity.show = model.Show;
            entity.type = Convert.ToInt32(model.Type);
            return await this._menuRepository.UpdateAsync(entity) > 0;
        }

        /// <summary>
        /// 获取当前用户可访问的菜单列表集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<MenuModel>> GetCurrentUserMenuList(string userId)
        {
            var menus = await ((SysMenuRepository)_menuRepository).GetCurrentUserMenuList(userId);
            return ToTree(menus.Select(p => new MenuModel()
            {
                Id = p.id,
                ParentId = p.parentid,
                Path = p.path,
                Component = p.component,
                Meta = Newtonsoft.Json.JsonConvert.DeserializeObject<MenuMetaData>(p.meta),
                Name = p.name,
                Redirect = p.redirect
            }).ToList(), "00000000000000000000000000000000");
        }

        public async Task<List<MenuModel>> GetMenuList(MenuListParam param)
        {
            var menus = await this._menuRepository.Select
               .WhereIf(!string.IsNullOrWhiteSpace(param.MenuName), p => p.name.Equals(param.MenuName))
               .WhereIf(int.TryParse(param.Status, out int _), p => p.status == int.Parse(param.Status))
               .Page(param.Page, param.PageSize)
               .ToListAsync();
            return ToTree(menus.Select(p =>
            {
                var meta = Newtonsoft.Json.JsonConvert.DeserializeObject<MenuMetaData>(p.meta);
                return new MenuModel()
                {
                    Id = p.id,
                    Component = p.component,
                    CreateTime = p.createtime,
                    Icon = p.icon,
                    Name = p.name,
                    OrderNo = p.orderno,
                    ParentId = p.parentid,
                    Permission = p.permission,
                    Status = p.status.ToString(),
                    Show = p.show.ToString(),
                    IsExt = p.isext.ToString(),
                    Type = p.type.ToString(),
                    Path = p.path,
                    Meta = meta,
                    Redirect = p.redirect,
                    ExtPath = meta?.FrameSrc
                };
            }).ToList(), "00000000000000000000000000000000");
        }

        private List<MenuModel> ToTree(List<MenuModel> menus, string parentId)
        {
            var currentMenus = menus.Where(p => p.ParentId == parentId);
            foreach(var menu in currentMenus)
            {
                var menuTree = ToTree(menus, menu.Id);
                if (menuTree.Count > 0)
                {
                    menu.Children = new List<MenuModel>();
                    menu.Children.AddRange(menuTree);
                }
            }
            return currentMenus?.ToList();
        }

        private MenuType GetMenuType(int menuType)
        {
            return (MenuType)menuType;
        }
    }
}
