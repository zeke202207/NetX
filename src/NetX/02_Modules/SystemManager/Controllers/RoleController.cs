using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.Common.Models;
using NetX.Swagger;
using NetX.SystemManager.Core;
using NetX.SystemManager.Models;
using NetX.SystemManager.Models.Dtos.RequestDto.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Controllers
{
    [ApiControllerDescriptionAttribute("SystemManager", Description = "NetX实现的系统管理模块->菜单管理")]
    public class RoleController : SystemManagerBaseController
    {
        private IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            this._roleService = roleService;
        }

        [ApiActionDescriptionAttribute("获取角色列表")]
        [HttpPost]
        public async Task<ActionResult> GetRoleListByPage(RoleListParam roleListparam)
        {
            var roles = await _roleService.GetRoleList(roleListparam);
            return new JsonResult(new ResultModel<List<RoleModel>>(ResultEnum.SUCCESS)
            {
                Result = roles
            });
        }

        [ApiActionDescription("获取全部角色列表")]
        [HttpGet]
        public async Task<ActionResult> GetAllRoleList()
        {
            var roles = await _roleService.GetRoleList();
            return new JsonResult(new ResultModel<List<RoleModel>>(ResultEnum.SUCCESS)
            {
                Result = roles
            });
        }

        /// <summary>
        /// add a new role
        /// </summary>
        /// <returns></returns>
        [ApiActionDescription("添加角色")]
        [HttpPost]
        public async Task<ActionResult> AddRole(RoleRequestModel model)
        {
            var result = await _roleService.AddRole(model);
            return new JsonResult(new ResultModel<bool>(ResultEnum.SUCCESS)
            {
                Result = result
            });
        }

        /// <summary>
        /// add a new role
        /// </summary>
        /// <returns></returns>
        [ApiActionDescription("更新角色")]
        [HttpPost]
        public async Task<ActionResult> UpdateRole(RoleRequestModel model)
        {
            var result = await _roleService.UpdateRole(model);
            return new JsonResult(new ResultModel<bool>(ResultEnum.SUCCESS)
            {
                Result = result
            });
        }

        /// <summary>
        /// add a new role
        /// </summary>
        /// <returns></returns>
        [ApiActionDescription("删除角色")]
        [HttpDelete]
        public async Task<ActionResult> RemoveRole(DeleteParam model)
        {
            var result = await _roleService.RemoveRole(model.Id);
            return new JsonResult(new ResultModel<bool>(ResultEnum.SUCCESS)
            {
                Result = result
            });
        }

        /// <summary>
        /// add a new role
        /// </summary>
        /// <returns></returns>
        [ApiActionDescription("更新角色状态")]
        [HttpPost]
        public async Task<ActionResult> SetRoleStatus(RoleStatusModel model)
        {
            var result = await _roleService.UpdateRoleStatus(model.Id,model.Status);
            return new JsonResult(new ResultModel<bool>(ResultEnum.SUCCESS)
            {
                Result = result
            });
        }
    }
}
