using Microsoft.Extensions.Caching.Memory;
using NetX.Cache.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace NetX.InMemoryCache;

/// <summary>
/// 内存缓存器
/// </summary>
public class MemoryCacheProvider : ICacheProvider
{
    private readonly IMemoryCache _memoryCache;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="momoryCache"></param>
    public MemoryCacheProvider(IMemoryCache momoryCache)
    {
        _memoryCache = momoryCache;
    }

    public bool Exists(string key)
    {
        return TryGetValue(key, out _);
    }

    public bool Exists<T>(string key)
    {
        return TryGetValue<T>(key, out _);
    }

    public Task<bool> ExistsAsync(string key)
    {
        return Task.FromResult(TryGetValue(key, out _));
    }

    public string Get(string key)
    {
        return _memoryCache.Get(key)?.ToString();
    }

    public T Get<T>(string key)
    {
        return _memoryCache.Get<T>(key);
    }

    private List<string> GetAllKeys()
    {
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
        var entries = _memoryCache.GetType().GetField("_entries", flags).GetValue(_memoryCache);
        var cacheItems = entries as IDictionary;
        var keys = new List<string>();
        if (cacheItems == null) return keys;
        foreach (DictionaryEntry cacheItem in cacheItems)
        {
            keys.Add(cacheItem.Key.ToString());
        }
        return keys;
    }

    public Task<string> GetAsync(string key)
    {
        return Task.FromResult(Get(key));
    }

    public Task<T> GetAsync<T>(string key)
    {
        return Task.FromResult(Get<T>(key));
    }

    public bool Remove(string key)
    {
        _memoryCache.Remove(key);
        return true;
    }

    public Task<bool> RemoveAsync(string key)
    {
        return Task.FromResult(Remove(key));
    }

    public async Task RemoveByPrefixAsync(string prefix)
    {
        if (string.IsNullOrEmpty(prefix))
            return;
        var keys = GetAllKeys().Where(m => m.StartsWith(prefix));
        foreach (var key in keys)
        {
            await RemoveAsync(key);
        }
    }

    public bool Set<T>(string key, T value)
    {
        _memoryCache.Set<T>(key, value);
        return true;
    }

    public bool Set<T>(string key, T value, TimeSpan ts)
    {
        _memoryCache.Set<T>(key, value, ts);
        return true;
    }

    public Task<bool> SetAsync<T>(string key, T value)
    {
        return Task.FromResult(Set<T>(key, value));
    }

    public Task<bool> SetAsync<T>(string key, T value, TimeSpan ts)
    {
        return Task.FromResult(Set<T>(key, value, ts));
    }

    public bool TryGetValue(string key, out string value)
    {
        return _memoryCache.TryGetValue(key, out value);
    }

    public bool TryGetValue<T>(string key, out T value)
    {
        return _memoryCache.TryGetValue<T>(key, out value);
    }
}
