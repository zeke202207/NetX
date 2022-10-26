using System.Diagnostics.CodeAnalysis;

namespace NetX.RBAC;

/// <summary>
/// api比较器
/// </summary>
public class ApiEquelCompare : IEqualityComparer<string>
{
    /// <summary>
    /// 比较权限api列表中是否包含路由api地址
    /// </summary>
    /// <param name="x">The first object of type T to compare.</param>
    /// <param name="y">The seconde object of type T to compare.</param>
    /// <returns></returns>
    public bool Equals(string? x, string? y)
    {
        if (string.IsNullOrEmpty(x) || string.IsNullOrEmpty(y))
            return false;
        return x.ToLower().Contains(y.ToLower());
    }

    /// <summary>
    /// Returns a hash code for the specified object.
    /// </summary>
    /// <param name="obj">The System.Object for which a hash code is to be returned.</param>
    /// <returns></returns>
    public int GetHashCode([DisallowNull] string obj)
    {
        return base.GetHashCode();
    }
}
