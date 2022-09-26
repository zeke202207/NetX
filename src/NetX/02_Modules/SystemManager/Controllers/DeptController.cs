using Microsoft.AspNetCore.Mvc;
using NetX.Common.Models;
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
    [ApiActionDescription("获取部门列表")]
    [HttpGet]
    public async Task<ResultModel<List<DeptModel>>> GetDeptList([FromQuery] DeptListParam queryParam)
    {
        return await _deptService.GetDeptList(queryParam);
    }

    /// <summary>
    /// 新增部门信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [ApiActionDescription("新增部门信息")]
    [HttpPost]
    public async Task<ResultModel<bool>> AddDept(DeptRequestModel model)
    {
        return await _deptService.AddDept(model);
    }

    /// <summary>
    /// 编辑部门信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [ApiActionDescription("编辑部门信息")]
    [HttpPost]
    public async Task<ResultModel<bool>> UpdateDept(DeptRequestModel model)
    {
        return await _deptService.UpdateDept(model);
    }

    /// <summary>
    /// 删除部门信息
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    [ApiActionDescription("删除部门信息")]
    [HttpDelete]
    public async Task<ResultModel<bool>> RemoveDept(DeleteParam param)
    {
        return await _deptService.RemoveDept(param.Id);
    }
}
