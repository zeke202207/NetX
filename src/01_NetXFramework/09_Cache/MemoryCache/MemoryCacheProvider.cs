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
    /// 内存缓存器实例
    /// </summary>
    /// <param name="momoryCache"></param>
    public MemoryCacheProvider(IMemoryCache momoryCache)
    {
        _memoryCache = momoryCache;
    }

    /// <summary>
    /// 缓存key是否存在
    /// </summary>
    /// <param name="key">缓存key</param>
    /// <returns></returns>
    public bool Exists(string key)
    {
        return TryGetValue(key, out _);
    }

    /// <summary>
    /// 缓存key是否存在
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool Exists<T>(string key)
    {
        return TryGetValue<T>(key, out _);
    }

    /// <summary>
    /// 缓存key是否存在
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public Task<bool> ExistsAsync(string key)
    {
        return Task.FromResult(TryGetValue(key, out _));
    }

    /// <summary>
    /// 根据key获取缓存value
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string Get(string key)
    {
        return _memoryCache.Get(key)?.ToString();
    }

    /// <summary>
    /// 根据key获取缓存value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public T Get<T>(string key)
    {
        return _memoryCache.Get<T>(key);
    }

    /// <summary>
    /// 获取全部缓存key集合
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// 根据key获取缓存value
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public Task<string> GetAsync(string key)
    {
        return Task.FromResult(Get(key));
    }

    /// <summary>
    /// 根据key获取缓存value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public Task<T> GetAsync<T>(string key)
    {
        return Task.FromResult(Get<T>(key));
    }

    /// <summary>
    /// 移除缓存key所有内容
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool Remove(string key)
    {
        _memoryCache.Remove(key);
        return true;
    }

    /// <summary>
    /// 移除缓存key所有内容
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public Task<bool> RemoveAsync(string key)
    {
        return Task.FromResult(Remove(key));
    }

    /// <summary>
    /// 移除缓存前缀为<code>prefix</code>的key所有内容
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool Set<T>(string key, T value)
    {
        _memoryCache.Set<T>(key, value);
        return true;
    }

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="ts"></param>
    /// <returns></returns>
    public bool Set<T>(string key, T value, TimeSpan ts)
    {
        _memoryCache.Set<T>(key, value, ts);
        return true;
    }

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public Task<bool> SetAsync<T>(string key, T value)
    {
        return Task.FromResult(Set<T>(key, value));
    }

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="ts"></param>
    /// <returns></returns>
    public Task<bool> SetAsync<T>(string key, T value, TimeSpan ts)
    {
        return Task.FromResult(Set<T>(key, value, ts));
    }

    /// <summary>
    /// 根据缓存key获取缓存内容
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TryGetValue(string key, out string value)
    {
        return _memoryCache.TryGetValue(key, out value);
    }

    /// <summary>
    /// 根据缓存key获取缓存内容
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TryGetValue<T>(string key, out T value)
    {
        return _memoryCache.TryGetValue<T>(key, out value);
    }
}
