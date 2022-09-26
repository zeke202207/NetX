﻿using NetX.Common.Models;
using NetX.SystemManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Core;

/// <summary>
/// Api服务接口
/// </summary>
public interface IApiService
{
    /// <summary>
    /// 获取Api列表
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    Task<ResultModel<PagerResultModel<List<ApiModel>>>> GetApiList(ApiListParam param);

    /// <summary>
    /// 新增Api
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> AddApi(ApiRequestModel model);

    /// <summary>
    /// 更新Api
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> UpdateApi(ApiRequestModel model);

    /// <summary>
    /// 删除Api
    /// </summary>
    /// <param name="apiId"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> RemoveApi(string apiId);

}
