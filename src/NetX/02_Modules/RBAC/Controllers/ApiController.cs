using Microsoft.AspNetCore.Mvc;
using NetX.Common.ModuleInfrastructure;
using NetX.Swagger;
using NetX.RBAC.Core;
using NetX.RBAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetX.Logging.Monitors;

namespace NetX.RBAC.Controllers;

/// <summary>
/// 系统Api管理对外开放api接口
/// </summary>
[ApiControllerDescription(RBACConst.C_RBAC_GROUPNAME, Description = "NetX实现的系统管理模块->接口管理")]
public class ApiController : RBACBaseController
{
    private IApiService _apiService;

    /// <summary>
    /// 角色管理api实例对象
    /// </summary>
    /// <param name="apiService">api服务</param>
    public ApiController(IApiService apiService)
    {
        this._apiService = apiService;
    }

    /// <summary>
    /// 获取API分页列表
    /// </summary>
    /// <param name="apiPageParam">分页查询条件</param>
    /// <returns></returns>
    [ApiActionDescription("获取API分页列表")]
    [HttpPost]
    public async Task<ResultModel> GetApiPageList(ApiPageParam apiPageParam)
    {
        return await _apiService.GetApiList(apiPageParam);
    }

    /// <summary>
    /// 获取API列表
    /// </summary>
    /// <param name="apiParam">查询条件</param>
    /// <returns></returns>
    [ApiActionDescription("获取API列表")]
    [HttpPost]
    public async Task<ResultModel> GetApiList(ApiParam apiParam)
    {
        return await _apiService.GetApiList(apiParam);
    }

    /// <summary>
    /// 添加API
    /// </summary>
    /// <param name="model">API实体对象</param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("添加API")]
    [HttpPost]
    public async Task<ResultModel> AddApi(ApiRequestModel model)
    {
        return await this._apiService.AddApi(model);
    }

    /// <summary>
    /// 编辑API信息
    /// </summary>
    /// <param name="model">API实体对象</param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("编辑API信息")]
    [HttpPost]
    public async Task<ResultModel> UpdateApi(ApiRequestModel model)
    {
        return await this._apiService.UpdateApi(model);
    }

    /// <summary>
    /// 删除API
    /// </summary>
    /// <param name="param">删除主键信息</param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("删除API")]
    [HttpDelete]
    public async Task<ResultModel> RemoveApi(KeyParam param)
    {
        return await this._apiService.RemoveApi(param.Id);
    }
}
