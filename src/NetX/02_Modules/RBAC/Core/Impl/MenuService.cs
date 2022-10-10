﻿using AutoMapper;
using FreeSql;
using NetX.Common.Attributes;
using NetX.Common.Models;
using NetX.RBAC.Data.Repositories;
using NetX.RBAC.Models;
using Newtonsoft.Json;
using System.Data;

namespace NetX.RBAC.Core;

/// <summary>
/// 菜单管理服务
/// </summary>
[Scoped]
public class MenuService : RBACBaseService, IMenuService
{
    private readonly IBaseRepository<sys_menu> _menuRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// 菜单管理实例对象
    /// </summary>
    /// <param name="menuRepository"></param>
    /// <param name="mapper"></param>
    public MenuService(
        IBaseRepository<sys_menu> menuRepository,
        IMapper mapper)
    {
        this._menuRepository = menuRepository;
        this._mapper = mapper;
    }

    /// <summary>
    /// 新增菜单
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> AddMenu(MenuRequestModel model)
    {
        var menuType = GetMenuType(int.Parse(model.Type));
        var menuEntity = ToEntity(model);
        menuEntity.id = base.CreateId();
        menuEntity.createtime = base.CreateInsertTime();
        await this._menuRepository.InsertAsync(menuEntity);
        return base.Success<bool>(true);
    }

    /// <summary>
    /// 跟新菜单信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> UpdateMenu(MenuRequestModel model)
    {
        var menuType = GetMenuType(int.Parse(model.Type));
        var entity = await this._menuRepository.Select.Where(p => p.id.Equals(model.Id)).FirstAsync();
        var menuEntity = ToEntity(model);
        menuEntity.id = model.Id;
        menuEntity.createtime = entity.createtime;
        var result = await this._menuRepository.UpdateAsync(menuEntity) > 0;
        return base.Success<bool>(result);
    }

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="menuId"></param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> RemoveMenu(string menuId)
    {
        var result = await ((SysMenuRepository)this._menuRepository).RemoveMenuAsync(menuId);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// 获取当前用户可访问的菜单列表集合
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<ResultModel<List<MenuModel>>> GetCurrentUserMenuList(string userId)
    {
        var menus = await ((SysMenuRepository)_menuRepository).GetCurrentUserMenuListAsync(userId);
        var result = ToTree(this._mapper.Map<List<MenuModel>>(menus), RBACConst.C_ROOT_ID);
        return base.Success<List<MenuModel>>(result);
    }

    /// <summary>
    /// 获取菜单集合
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public async Task<ResultModel<List<MenuModel>>> GetMenuList(MenuListParam param)
    {
        var menus = await this._menuRepository.Select
           .WhereIf(!string.IsNullOrWhiteSpace(param.MenuName), p => p.name.Equals(param.MenuName))
           .WhereIf(int.TryParse(param.Status, out int _), p => p.status == int.Parse(param.Status))
           .Page(param.Page, param.PageSize)
           .ToListAsync();
        var result = ToTree(this._mapper.Map<List<MenuModel>>(menus), RBACConst.C_ROOT_ID);
        return base.Success<List<MenuModel>>(result);
    }

    /// <summary>
    /// 生产菜单树
    /// </summary>
    /// <param name="menus"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    private List<MenuModel> ToTree(List<MenuModel> menus, string parentId)
    {
        var currentMenus = menus.Where(p => p.ParentId == parentId);
        if (null == currentMenus)
            return new List<MenuModel>();
        foreach (var menu in currentMenus)
        {
            var menuTree = ToTree(menus, menu.Id);
            if (menuTree.Count > 0)
            {
                menu.Children = new List<MenuModel>();
                menu.Children.AddRange(menuTree);
            }
        }
        return currentMenus.ToList();
    }

    /// <summary>
    /// 转换数据库实体对象
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    private sys_menu ToEntity(MenuRequestModel model)
    {
        var menuType = GetMenuType(int.Parse(model.Type));
        var menuEntity = this._mapper.Map<sys_menu>(model);
        menuEntity.parentid = string.IsNullOrWhiteSpace(model.ParentId) ? RBACConst.C_ROOT_ID : model.ParentId;
        menuEntity.component = (Ext)model.IsExt == Ext.Yes ?
            "IFrame" : menuType == MenuType.Dir ?
            "LAYOUT" : string.IsNullOrWhiteSpace(model.Component) ? "" : model.Component;
        menuEntity.path = string.IsNullOrWhiteSpace(model.Path) ?
            "" : menuType == MenuType.Dir ?
            model.Path.StartsWith("/") ?
            model.Path : $"/{model.Path}" : model.Path;
        menuEntity.meta = JsonConvert.SerializeObject(this._mapper.Map<MenuMetaData>(model));
        return menuEntity;
    }

    /// <summary>
    /// 获取菜单类型
    /// </summary>
    /// <param name="menuType"></param>
    /// <returns></returns>
    private MenuType GetMenuType(int menuType)
    {
        return (MenuType)menuType;
    }
}