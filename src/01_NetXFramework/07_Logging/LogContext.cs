using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Logging;

/// <summary>
/// 
/// </summary>
public sealed class LogContext
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LogContext()
    {
    }

    /// <summary>
    /// 日志上下文数据
    /// </summary>
    public IDictionary<object, object> Properties { get; set; }

    /// <summary>
    /// 设置上下文数据
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public LogContext Set(object key, object value)
    {
        Properties ??= new ConcurrentDictionary<object, object>();
        Properties.TryAdd(key, value);
        return this;
    }

    /// <summary>
    /// 批量设置上下文数据
    /// </summary>
    /// <param name="properties"></param>
    /// <returns></returns>
    public LogContext SetRange(IDictionary<object, object> properties)
    {
        if (properties == null || properties.Count == 0) 
            return this;
        Properties ??= new ConcurrentDictionary<object, object>();
        foreach (var (key, value) in properties)
        {
            Properties.TryAdd(key, value);
        }
        return this;
    }

    /// <summary>
    /// 获取上下文数据
    /// </summary>
    /// <param name="key">键</param>
    /// <returns></returns>
    public object Get(object key)
    {
        if (Properties == null || Properties.Count == 0) 
            return default;
        var isExists = Properties.TryGetValue(key, out var value);
        return isExists ? value : null;
    }

    /// <summary>
    /// 获取上下文数据
    /// </summary>
    /// <param name="key">键</param>
    /// <returns></returns>
    public T Get<T>(object key)
    {
        var value = Get(key);
        return (T)value;
    }
}
