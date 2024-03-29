﻿using NetX.Cache.Core;
using NetX.Caching.Models.Dtos;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.Ddd.Domain;

namespace NetX.Caching.Domain
{
    [Scoped]
    public class CachingKeyQueryHandler : DomainQueryHandler<CachingKeyQuery, ResultModel>
    {
        private readonly Func<string, ICacheProvider> _cacheProviderFactory;

        public CachingKeyQueryHandler(IDatabaseContext dbContext, Func<string, ICacheProvider> cacheProviderFactory)
            : base(dbContext)
        {
            _cacheProviderFactory = cacheProviderFactory;
        }

        public override async Task<ResultModel> Handle(CachingKeyQuery request, CancellationToken cancellationToken)
        {
            var provider = _cacheProviderFactory(request.CacheType);
            var keys = await provider.GetKeys();
            List<CachingKeyModel> result = new List<CachingKeyModel>();
            foreach (var item in keys)
            {
                var splitItem = item.Split(':');
                //过滤非约定缓存管理:约定格式： 模块:业务：缓存
                if (splitItem?.Count() <= 1)
                    continue;
                var prefixKey = string.Join(":", splitItem, 0, splitItem.Length - 1);
                var key = splitItem[splitItem.Length - 1];
                CachingKeyModel model = new CachingKeyModel();
                if (result.Exists(p => p.CachingPrefixKey == prefixKey))
                    model = result.FirstOrDefault(p => p.CachingPrefixKey == prefixKey);
                else
                {
                    model.CachingPrefixKey = prefixKey;
                    result.Add(model);
                }
                model.Keys.Add(new CachingKey() { Key = key });
            }
            return result.ToSuccessResultModel();
            //return keys.Select(p => new CachingKeyModel() { CachingKey = p, CacheType = request.CacheType }).ToSuccessResultModel();
        }
    }
}
