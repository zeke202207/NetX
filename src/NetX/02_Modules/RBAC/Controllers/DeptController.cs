using Microsoft.AspNetCore.Mvc;
using NetX.AuditLog;
using NetX.Common.ModuleInfrastructure;
using NetX.Ddd.Core;
using NetX.RBAC.Domain;
using NetX.RBAC.Models;
using NetX.Swagger;

namespace NetX.RBAC.Controllers;

/// <summary>
/// 部门管理api接口
/// </summary>
[ApiControllerDescription(RBACConst.C_RBAC_GROUPNAME, Description = "NetX实现的系统管理模块->部门管理")]
public class DeptController : RBACBaseController
{
    private readonly IQueryBus _deptQuery;
    private readonly ICommandBus _deptCommand;

    /// <summary>
    /// 部门管理api实例对象
    /// </summary>
    public DeptController(IQueryBus deptQuery, ICommandBus deptCommand)
    {
        this._deptQuery = deptQuery;
        this._deptCommand = deptCommand;
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
        return await _deptQuery.Send<DeptPagerListQuery, ResultModel>(new DeptPagerListQuery(queryParam.ContainDisabled, queryParam.DeptName, queryParam.Status, 1, 999));
    }

    /// <summary>
    /// 新增部门信息
    /// </summary>
    /// <param name="model">部门实体对象</param>
    /// <returns></returns>
    [Audited]
    [ApiActionDescription("新增部门信息")]
    [HttpPost]
    public async Task<ResultModel> AddDept(DeptRequestModel model)
    {
        await _deptCommand.Send<DeptAddCommand>(new DeptAddCommand(model.ParentId, model.DeptName, model.Status, model.OrderNo, model.Remark));
        return true.ToSuccessResultModel();
    }

    /// <summary>
    /// 编辑部门信息
    /// </summary>
    /// <param name="model">部门实体对象</param>
    /// <returns></returns>
    [Audited]
    [ApiActionDescription("编辑部门信息")]
    [HttpPost]
    public async Task<ResultModel> UpdateDept(DeptRequestModel model)
    {
        await _deptCommand.Send<DeptModifyCommand>(new DeptModifyCommand(model.Id, model.ParentId, model.DeptName, model.Status, model.OrderNo, model.Remark));
        return true.ToSuccessResultModel();
    }

    /// <summary>
    /// 删除部门信息
    /// </summary>
    /// <param name="param">删除实体</param>
    /// <returns></returns>
    [Audited]
    [ApiActionDescription("删除部门信息")]
    [HttpDelete]
    public async Task<ResultModel> RemoveDept(KeyParam param)
    {
        await _deptCommand.Send<DeptRemoveCommand>(new DeptRemoveCommand(param.Id));
        return true.ToSuccessResultModel();
    }
}
