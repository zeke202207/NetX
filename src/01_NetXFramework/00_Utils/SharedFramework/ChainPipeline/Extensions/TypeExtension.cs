namespace NetX.SharedFramework.ChainPipeline;

/// <summary>
/// 类型扩展类
/// </summary>
public static class TypeExtension
{
    /// <summary>
    /// 判断指定类型是否实现于该类型
    /// </summary>
    /// <param name="serviceType"></param>
    /// <param name="implementType"></param>
    /// <returns></returns>
    public static bool IsImplementType(this Type serviceType, Type implementType)
    {
        //泛型
        if (serviceType.IsGenericType)
        {
            if (serviceType.IsInterface)
            {
                var interfaces = implementType.GetInterfaces();
                if (interfaces.Any(m => m.IsGenericType && m.GetGenericTypeDefinition() == serviceType))
                    return true;
            }
            else
            {
                if (implementType.BaseType != null && implementType.BaseType.IsGenericType &&
                    implementType.BaseType.GetGenericTypeDefinition() == serviceType)
                    return true;
            }
        }
        else
        {
            if (serviceType.IsInterface)
            {
                var interfaces = implementType.GetInterfaces();
                if (interfaces.Any(m => m == serviceType))
                    return true;
            }
            else
            {
                if (implementType.BaseType != null && implementType.BaseType == serviceType)
                    return true;
            }
        }
        return false;
    }
}
