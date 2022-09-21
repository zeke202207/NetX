using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.Common.Models;
using NetX.Swagger;
using NetX.SystemManager.Core;
using NetX.SystemManager.Models;
using NetX.SystemManager.Models.Dtos.RequestDto.Param;
using NetX.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    [ApiControllerDescriptionAttribute("SystemManager", Description = "NetX实现的系统管理模块->菜单管理")]
    public class MenuController : SystemManagerBaseController
    {
        private IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            this._menuService = menuService;    
        }

        [ApiActionDescriptionAttribute("获取登录用户授权菜单列表")]
        [NoPermission]
        [HttpGet]
        public async Task<ActionResult> GetCurrentUserMenuList()
        {
            var result = await this._menuService.GetCurrentUserMenuList(TenantContext.CurrentTenant.Principal.UserId);
            return new JsonResult(new ResultModel<List<MenuModel>>(ResultEnum.SUCCESS)
            {
                Result = result
            });
        }

        [ApiActionDescriptionAttribute("获取登录用户授权菜单列表")]
        [NoPermission]
        [HttpPost]
        public async Task<ActionResult> GetMenuList(MenuListParam param)
        {
            var result = await this._menuService.GetMenuList(param);
            return new JsonResult(new ResultModel<List<MenuModel>>(ResultEnum.SUCCESS)
            {
                Result = result
            });
        }

        [ApiActionDescriptionAttribute("添加菜单")]
        [HttpPost]
        public async Task<ActionResult> AddMenu(MenuRequestModel model)
        {
            var result = await this._menuService.AddMenu(model);
            return new JsonResult(new ResultModel<bool>(ResultEnum.SUCCESS)
            {
                Result = result
            });
        }

        [ApiActionDescriptionAttribute("编辑菜单")]
        [HttpPost]
        public async Task<ActionResult> UpdateMenu(MenuRequestModel model)
        {
            var result = await this._menuService.UpdateMenu(model);
            return new JsonResult(new ResultModel<bool>(ResultEnum.SUCCESS)
            {
                Result = result
            });
        }

        [ApiActionDescriptionAttribute("删除菜单")]
        [HttpDelete]
        public async Task<ActionResult> RemoveMenu(DeleteParam param)
        {
            var result = await this._menuService.RemoveMenu(param.Id);
            return new JsonResult(new ResultModel<bool>(ResultEnum.SUCCESS)
            {
                Result = result
            });
        }
    }
}
