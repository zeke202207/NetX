using NetX.RBAC.Models;
using Quartz.Util;
using System.Diagnostics.CodeAnalysis;

namespace NetX.RBAC;

/// <summary>
/// api比较器
/// </summary>
public class ApiEquelCompare : IEqualityComparer<PermissionCacheApiModel>
{
    /// <summary>
    /// 比较权限api列表中是否包含路由api地址
    /// </summary>
    /// <param name="x">The first object of type T to compare.</param>
    /// <param name="y">The seconde object of type T to compare.</param>
    /// <returns></returns>
    public bool Equals(PermissionCacheApiModel? x, PermissionCacheApiModel? y)
    {
        if (x == null || y == null || 
            x.Path.IsNullOrWhiteSpace() || x.Method.IsNullOrWhiteSpace() || 
            y.Path.IsNullOrWhiteSpace() || y.Method.IsNullOrWhiteSpace())
            return false;
        return x.Path.ToLower().Contains(y.Path.ToLower()) && x.Method.ToLower().Equals(y.Method.ToLower());
    }

    /// <summary>
    /// Returns a hash code for the specified object.
    /// </summary>
    /// <param name="obj">The System.Object for which a hash code is to be returned.</param>
    /// <returns></returns>
    public int GetHashCode([DisallowNull] PermissionCacheApiModel obj)
    {
        return base.GetHashCode();
    }
}
