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
    /// <summary>
    /// 部门管理
    /// </summary>
    [ApiControllerDescriptionAttribute("SystemManager", Description = "NetX实现的系统管理模块->部门管理")]
    public class DeptController : SystemManagerBaseController
    {
        private readonly IDeptService _deptService;

        public DeptController(IDeptService deptService)
        {
            _deptService = deptService;
        }

        [ApiActionDescriptionAttribute("获取部门列表")]
        [HttpGet]
        public async Task<ActionResult> GetDeptList([FromQuery] DeptListParam queryParam)
        {
            var result = await _deptService.GetDeptList(queryParam);
            return new JsonResult(new ResultModel<List<DeptModel>>(ResultEnum.SUCCESS)
            {
                Message = "",
                Result = result
            });
        }

        [ApiActionDescriptionAttribute("add a new department")]
        [HttpPost]
        public async Task<ActionResult> AddDept(DeptRequestModel model)
        {
            var result = await _deptService.AddDept(model);
            return new JsonResult(new ResultModel<bool>(ResultEnum.SUCCESS)
            {
                Message = "",
                Result = result
            });
        }

        [ApiActionDescriptionAttribute("edit department")]
        [HttpPost]
        public async Task<ActionResult> UpdateDept(DeptRequestModel model)
        {
            var result = await _deptService.UpdateDept(model);
            return new JsonResult(new ResultModel<bool>(ResultEnum.SUCCESS)
            {
                Message = "",
                Result = result
            });
        }

        [ApiActionDescriptionAttribute("delete department")]
        [HttpDelete]
        public async Task<ActionResult>RemoveDept(DeleteParam param)
        {
            var result = await _deptService.RemoveDept(param.Id);
            return new JsonResult(new ResultModel<bool>(ResultEnum.SUCCESS)
            {
                Message = "",
                Result = result
            });
        }
    }
}
