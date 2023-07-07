using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetX.Cache.Core;

/// <summary>
/// 
/// </summary>
public interface ICacheProvider
{
    string CacheType { get; }

    string CacheName { get; }

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="key">键</param>
    /// <returns></returns>
    object Get(string key);

    /// <summary>
    /// 获取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">键</param>
    /// <returns></returns>
    T Get<T>(string key);

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="key">键</param>
    /// <returns></returns>
    Task<object> GetAsync(string key);

    /// <summary>
    /// 获取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">键</param>
    /// <returns></returns>
    Task<T> GetAsync<T>(string key);

    /// <summary>
    /// 尝试获取
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">返回值</param>
    /// <returns></returns>
    bool TryGetValue(string key, out object value);

    /// <summary>
    /// 尝试获取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">键</param>
    /// <param name="value">返回值</param>
    /// <returns></returns>
    bool TryGetValue<T>(string key, out T value);

    /// <summary>
    /// 设置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    bool Set<T>(string key, T value);

    /// <summary>
    /// 设置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="expires">有效期(分钟)</param>
    bool Set<T>(string key, T value, TimeSpan ts);

    /// <summary>
    /// 设置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    Task<bool> SetAsync<T>(string key, T value);

    /// <summary>
    /// 设置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="expires">有效期(分钟)</param>
    /// <returns></returns>
    Task<bool> SetAsync<T>(string key, T value, TimeSpan ts);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="key"></param>
    bool Remove(string key);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<bool> RemoveAsync(string key);

    /// <summary>
    /// 删除所有前缀<paramref name="prefix"/>的缓存
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
    Task RemoveByPrefixAsync(string prefix);

    /// <summary>
    /// 指定键是否存在
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool Exists(string key);

    /// <summary>
    /// 指定键是否存在
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool Exists<T>(string key);

    /// <summary>
    /// 指定键是否存在
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(string key);

    /// <summary>
    /// 指定键是否存在
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync<T>(string key);

    /// <summary>
    /// 获取所有的key
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<string>> GetKeys();
}
