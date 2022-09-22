using Microsoft.AspNetCore.Mvc;
using NetX.Swagger;
using NetX.SystemManager.Core;
using NetX.SystemManager.Models;

namespace NetX.SystemManager.Controllers;

/// <summary>
/// 部门管理api接口
/// </summary>
[ApiControllerDescription("SystemManager", Description = "NetX实现的系统管理模块->部门管理")]
public class DeptController : SystemManagerBaseController
{
    private readonly IDeptService _deptService;

    /// <summary>
    /// 部门管理api实例对象
    /// </summary>
    /// <param name="deptService"></param>
    public DeptController(IDeptService deptService)
    {
        _deptService = deptService;
    }

    /// <summary>
    /// 获取部门列表
    /// </summary>
    /// <param name="queryParam"></param>
    /// <returns></returns>
    [ApiActionDescriptionAttribute("获取部门列表")]
    [HttpGet]
    public async Task<ActionResult> GetDeptList([FromQuery] DeptListParam queryParam)
    {
        var result = await _deptService.GetDeptList(queryParam);
        return base.Success<List<DeptModel>>(result);
    }

    /// <summary>
    /// 新增部门信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [ApiActionDescriptionAttribute("新增部门信息")]
    [HttpPost]
    public async Task<ActionResult> AddDept(DeptRequestModel model)
    {
        var result = await _deptService.AddDept(model);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// 编辑部门信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [ApiActionDescriptionAttribute("编辑部门信息")]
    [HttpPost]
    public async Task<ActionResult> UpdateDept(DeptRequestModel model)
    {
        var result = await _deptService.UpdateDept(model);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// 删除部门信息
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    [ApiActionDescriptionAttribute("删除部门信息")]
    [HttpDelete]
    public async Task<ActionResult> RemoveDept(DeleteParam param)
    {
        var result = await _deptService.RemoveDept(param.Id);
        return base.Success<bool>(result);
    }
}
