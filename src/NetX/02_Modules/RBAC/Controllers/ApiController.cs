using Microsoft.AspNetCore.Mvc;
using NetX.Common.Models;
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
/// 
/// </summary>
[ApiControllerDescription(RBACConst.C_RBAC_GROUPNAME, Description = "NetX实现的系统管理模块->接口管理")]
public class ApiController : RBACBaseController
{
    private IApiService _apiService;

    /// <summary>
    /// 角色管理api实例对象
    /// </summary>
    /// <param name="apiService"></param>
    public ApiController(IApiService apiService)
    {
        this._apiService = apiService;
    }

    /// <summary>
    /// 获取api列表
    /// </summary>
    /// <param name="apiPageParam"></param>
    /// <returns></returns>
    [ApiActionDescription("获取api分页列表")]
    [HttpPost]
    public async Task<ResultModel<PagerResultModel<List<ApiModel>>>> GetApiPageList(ApiPageParam apiPageParam)
    {
        return await _apiService.GetApiList(apiPageParam);
    }

    /// <summary>
    /// 获取api列表
    /// </summary>
    /// <param name="apiParam"></param>
    /// <returns></returns>
    [ApiActionDescription("获取api列表")]
    [HttpPost]
    public async Task<ResultModel<List<ApiModel>>> GetApiList(ApiParam apiParam)
    {
        return await _apiService.GetApiList(apiParam);
    }

    /// <summary>
    /// 添加菜单
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("添加api")]
    [HttpPost]
    public async Task<ResultModel<bool>> AddApi(ApiRequestModel model)
    {
        return await this._apiService.AddApi(model);
    }

    /// <summary>
    /// 编辑菜单
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("编辑api")]
    [HttpPost]
    public async Task<ResultModel<bool>> UpdateApi(ApiRequestModel model)
    {
        return await this._apiService.UpdateApi(model);
    }

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("删除api")]
    [HttpDelete]
    public async Task<ResultModel<bool>> RemoveApi(KeyParam param)
    {
        return await this._apiService.RemoveApi(param.Id);
    }
}
