using Microsoft.AspNetCore.Mvc;
using Netx.Ddd.Core;
using NetX.Common.ModuleInfrastructure;
using NetX.RBAC.Domain;
using NetX.RBAC.Models;
using NetX.Swagger;

namespace NetX.RBAC.Controllers;

/// <summary>
/// 系统Api管理对外开放api接口
/// </summary>
[ApiControllerDescription(RBACConst.C_RBAC_GROUPNAME, Description = "NetX实现的系统管理模块->接口管理")]
public class ApiController : RBACBaseController
{
    private readonly IQueryBus _apiQuery;
    private readonly ICommandBus _apiCommand;

    /// <summary>
    /// 角色管理api实例对象
    /// </summary>
    public ApiController(IQueryBus apiQuery, ICommandBus apiCommand)
    {
        this._apiQuery = apiQuery;
        this._apiCommand = apiCommand;
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
        return await _apiQuery.Send<ApiPagerListQuery, ResultModel>(new ApiPagerListQuery(apiPageParam.Ggroup, apiPageParam.CurrentPage, apiPageParam.PageSize));
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
        return await _apiQuery.Send<ApiListQuery, ResultModel>(new ApiListQuery());
    }

    /// <summary>
    /// 添加API
    /// </summary>
    /// <param name="model">API实体对象</param>
    /// <returns></returns>
    //[Audit]
    [ApiActionDescription("添加API")]
    [HttpPost]
    public async Task<ResultModel> AddApi(ApiRequestModel model)
    {
        await _apiCommand.Send<ApiAddCommand>(new ApiAddCommand(model.Path, model.Group, model.Method, model.Description)); ;
        return true.ToSuccessResultModel();
    }

    /// <summary>
    /// 编辑API信息
    /// </summary>
    /// <param name="model">API实体对象</param>
    /// <returns></returns>
    //[Audit]
    [ApiActionDescription("编辑API信息")]
    [HttpPost]
    public async Task<ResultModel> UpdateApi(ApiRequestModel model)
    {
        await _apiCommand.Send<ApiModifyCommand>(new ApiModifyCommand(model.Id, model.Path, model.Group, model.Method, model.Description));
        return true.ToSuccessResultModel();
    }

    /// <summary>
    /// 删除API
    /// </summary>
    /// <param name="param">删除主键信息</param>
    /// <returns></returns>
    //[Audit]
    [ApiActionDescription("删除API")]
    [HttpDelete]
    public async Task<ResultModel> RemoveApi(KeyParam param)
    {
        await _apiCommand.Send<ApiRemoveCommand>(new ApiRemoveCommand(param.Id));
        return true.ToSuccessResultModel();
    }
}
