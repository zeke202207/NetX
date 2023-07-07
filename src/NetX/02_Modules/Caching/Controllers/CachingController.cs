using Microsoft.AspNetCore.Mvc;
using NetX.Common.ModuleInfrastructure;
using Netx.Ddd.Core;
using NetX.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetX.Authentication.Core;
using NetX.Caching.Domain;
using NetX.Caching.Models;

namespace NetX.Caching.Controllers
{
    [ApiControllerDescription(CachingConstEnum.C_Caching_GROUPNAME, Description = "NetX实现的缓存模块->缓存管理")]
    public class CachingController :BaseController
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;

        /// <summary>
        /// 
        /// </summary>
        public CachingController(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        /// <summary>
        /// 获取注入的全部缓存类型
        /// </summary>
        /// <returns></returns>
        [ApiActionDescription("获取注入的全部缓存类型")]
        [HttpGet]
        [NoPermission]
        public async Task<ResultModel> GetCachingTypes()
        {
            return await _queryBus.Send<CachingTypeQuery, ResultModel>(new CachingTypeQuery());
        }

        /// <summary>
        /// 获取指定缓存下的所有缓存KEY
        /// </summary>
        /// <returns></returns>
        [ApiActionDescription("获取指定缓存下的所有缓存KEY")]
        [HttpPost]
        [NoPermission]
        public async Task<ResultModel> GetCachingKeys(CachingTypeParam request)
        {
            return await _queryBus.Send<CachingKeyQuery, ResultModel>(new CachingKeyQuery(request.CachingTypeKey));
        }

        /// <summary>
        /// 获取指定缓存key下的内容
        /// </summary>
        /// <returns></returns>
        [ApiActionDescription("获取指定缓存key下的内容")]
        [HttpPost]
        [NoPermission]
        public async Task<ResultModel> GetCachingValue(CachingKeyParam request)
        {
            return await _queryBus.Send<CachingValueQuery, ResultModel>(new CachingValueQuery(request.CacheType, request.CacheKey));
        }

        /// <summary>
        /// 删除指定key缓存
        /// </summary>
        /// <returns></returns>
        [ApiActionDescription("删除指定key缓存")]
        [HttpDelete]
        [NoPermission]
        public async Task<ResultModel> RemoveCachingValue(CachingPreKeyParam request)
        {
            await _commandBus.Send<CachingRemoveByPreKeyCommand>(new CachingRemoveByPreKeyCommand(request.CacheType, request.CachePreKey));
            return true.ToSuccessResultModel();
        }
    }
}
