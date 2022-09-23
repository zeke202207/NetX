using Microsoft.AspNetCore.Mvc;
using NetX.Common.Models;
using NetX.SystemManager.Models;

namespace NetX.SystemManager.Core;

/// <summary>
/// 部门管理服务接口
/// </summary>
public interface IDeptService
{
    /// <summary>
    /// 获取部门列表
    /// </summary>
    /// <param name="queryParam"></param>
    /// <returns></returns>
    Task<ResultModel<List<DeptModel>>> GetDeptList([FromQuery] DeptListParam queryParam);

    /// <summary>
    /// 添加部门
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> AddDept(DeptRequestModel model);

    /// <summary>
    /// 更新部门信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> UpdateDept(DeptRequestModel model);

    /// <summary>
    /// 删除部门信息
    /// </summary>
    /// <param name="deptId"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> RemoveDept(string deptId);
}
