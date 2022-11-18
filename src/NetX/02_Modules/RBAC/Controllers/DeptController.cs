using Microsoft.AspNetCore.Mvc;
using NetX.Common.ModuleInfrastructure;
using NetX.Swagger;
using NetX.RBAC.Core;
using NetX.RBAC.Models;
using NetX.Logging.Monitors;

namespace NetX.RBAC.Controllers;

/// <summary>
/// 部门管理api接口
/// </summary>
[ApiControllerDescription(RBACConst.C_RBAC_GROUPNAME, Description = "NetX实现的系统管理模块->部门管理")]
public class DeptController : RBACBaseController
{
    private readonly IDeptService _deptService;

    /// <summary>
    /// 部门管理api实例对象
    /// </summary>
    /// <param name="deptService">部门管理服务</param>
    public DeptController(IDeptService deptService)
    {
        _deptService = deptService;
    }

    /// <summary>
    /// 获取部门列表
    /// </summary>
    /// <param name="queryParam">查询部门条件</param>
    /// <returns></returns>
    [ApiActionDescription("获取部门列表")]
    [HttpGet]
    public async Task<ResultModel> GetDeptList([FromQuery] DeptListParam queryParam)
    {
        return await _deptService.GetDeptList(queryParam);
    }

    /// <summary>
    /// 新增部门信息
    /// </summary>
    /// <param name="model">部门实体对象</param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("新增部门信息")]
    [HttpPost]
    public async Task<ResultModel> AddDept(DeptRequestModel model)
    {
        return await _deptService.AddDept(model);
    }

    /// <summary>
    /// 编辑部门信息
    /// </summary>
    /// <param name="model">部门实体对象</param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("编辑部门信息")]
    [HttpPost]
    public async Task<ResultModel> UpdateDept(DeptRequestModel model)
    {
        return await _deptService.UpdateDept(model);
    }

    /// <summary>
    /// 删除部门信息
    /// </summary>
    /// <param name="param">删除实体</param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("删除部门信息")]
    [HttpDelete]
    public async Task<ResultModel> RemoveDept(KeyParam param)
    {
        return await _deptService.RemoveDept(param.Id);
    }
}
