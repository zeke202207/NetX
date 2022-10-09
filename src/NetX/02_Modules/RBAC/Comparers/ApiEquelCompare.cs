using System.Diagnostics.CodeAnalysis;

namespace NetX.RBAC;

/// <summary>
/// api比较器
/// </summary>
public class ApiEquelCompare : IEqualityComparer<string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool Equals(string? x, string? y)
    {
        if (string.IsNullOrEmpty(x) || string.IsNullOrEmpty(y))
            return false;
        return x.ToLower().Contains(y.ToLower());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public int GetHashCode([DisallowNull] string obj)
    {
        return base.GetHashCode();
    }
}
