using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.Common.Models;
using NetX.Swagger;
using NetX.SystemManager.Core;
using NetX.SystemManager.Models;
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
                Message = "",
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
                Message = "",
                Result = roles
            });
        }

        private List<RoleModel> GetMockRole()
        {
            return new List<RoleModel>()
            {
                new RoleModel()
                {
                    Id ="1",
                    RoleName = "超级管理员",
                    RoleValue ="1",
                    Status = ((int)Status.Enable).ToString(),
                    OrderNo ="0",
                    CreateTime =DateTime.Now,
                    Menus = new List<string>(){"21","211" },
                    Remark ="hello,zeke"
                },
                new RoleModel()
                {
                    Id ="2",
                    RoleName = "管理员",
                    RoleValue ="2",
                    Status =((int)Status.Disabled).ToString(),
                    OrderNo ="1",
                    CreateTime =DateTime.Now
                }
            };
        }
    }
}
