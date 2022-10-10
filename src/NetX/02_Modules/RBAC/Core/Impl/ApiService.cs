using AutoMapper;
using FreeSql;
using NetX.Common.Attributes;
using NetX.Common.Models;
using NetX.RBAC.Data.Repositories;
using NetX.RBAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Core;

/// <summary>
/// 
/// </summary>
[Scoped]
public class ApiService : RBACBaseService, IApiService
{
    private readonly IBaseRepository<sys_api> _apiRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// api管理实例对象
    /// </summary>
    /// <param name="apiRepository"></param>
    /// <param name="mapper"></param>
    public ApiService(
        IBaseRepository<sys_api> apiRepository,
        IMapper mapper)
    {
        this._apiRepository = apiRepository;
        this._mapper = mapper;
    }

    /// <summary>
    /// 获取api列表
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<PagerResultModel<List<ApiModel>>>> GetApiList(ApiPageParam param)
    {
        var result = await this._apiRepository.Select
            .OrderBy(p => p.group)
            .Page(pageNumber: param.Page, pageSize: param.PageSize)
            .ToListAsync();
        var total = await this._apiRepository.Select.CountAsync();
        var resultModel = new PagerResultModel<List<ApiModel>>()
        {
            Items = this._mapper.Map<List<ApiModel>>(result),
            Total = (int)total
        };
        return base.Success<PagerResultModel<List<ApiModel>>>(resultModel);
    }

    /// <summary>
    /// 获取api列表
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<List<ApiModel>>> GetApiList(ApiParam param)
    {
        var result = await this._apiRepository.Select
            .OrderBy(p => p.group)
            .ToListAsync();
        return base.Success<List<ApiModel>>(this._mapper.Map<List<ApiModel>>(result));
    }

    /// <summary>
    /// 移除api
    /// </summary>
    /// <param name="apiId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<bool>> RemoveApi(string apiId)
    {
        var result = await ((SysApiRepository)this._apiRepository).RemoveApi(apiId);
        return base.Success(result);
    }

    /// <summary>
    /// 添加api
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<bool>> AddApi(ApiRequestModel model)
    {
        var entity = _mapper.Map<sys_api>(model);
        if (null == entity)
            return base.Error<bool>("实体转换失败，添加api失败");
        entity.id =base.CreateId();
        entity.createtime =base.CreateInsertTime();
        await this._apiRepository.InsertAsync(entity);
        return base.Success<bool>(true);
    }

    /// <summary>
    /// 更新api
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<bool>> UpdateApi(ApiRequestModel model)
    {
        var entity = await this._apiRepository.Select.Where(p => p.id.Equals(model.Id)).FirstAsync();
        if(null == entity)
            return base.Error<bool>("未找到需要更新的api");
        entity.method = model.Method;
        entity.path = model.Path;
        entity.group = model.Group;
        entity.description = model.Description ?? "";
        var result = await this._apiRepository.UpdateAsync(entity) > 0;
        return base.Success<bool>(result);
    }
}
