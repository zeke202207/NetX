using Microsoft.Extensions.DependencyInjection;
using NetX.Cache.Core;
using NetX.InMemoryCache;
using NetX.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetX.App;

/// <summary>
/// 内部扩展方法
/// </summary>
internal static class AppExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    internal static void AddCaches(this IServiceCollection services)
    {
        var moduleInitializers = App.GetModuleInitializer();
        List<CacheKeyDescriptor> list = new List<CacheKeyDescriptor>();
        foreach (var item in moduleInitializers)
        {
            AddCacheKey(list, item.Key, item.ModuleType, item.GetType().Assembly.GetTypes());
        }
        var definedTypes = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(p => p.FullName.Contains(AppConst.C_APP_CACHE_ASSEMBLY_NAME))
            ?.DefinedTypes.Select(p => p.AsType());
        if (null != definedTypes)
            AddCacheKey(list, ModuleSetupConst.C_SERVERHOST_MODULE_ID, ModuleType.FrameworkModule, definedTypes.ToArray());
        services.AddCacheProvider(() => list);
        services.AddInMemoryCache();
    }

    /// <summary>
    /// 添加fw其他模块缓存key集合
    /// </summary>
    /// <param name="list"></param>
    /// <param name="moduleId"></param>
    /// <param name="moduleType"></param>
    /// <param name="types"></param>
    private static void AddCacheKey(List<CacheKeyDescriptor> list, Guid moduleId, ModuleType moduleType, Type[] types)
    {
        var cacheKeysType = types.Where(m => m.FullName.Contains(AppConst.C_APP_CACHE_CLASS_NAME));
        foreach (var type in cacheKeysType)
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            foreach (var field in fields)
            {
                var module = GetModuleOption(moduleId, moduleType);
                if (null == field || null == module)
                    continue;
                var descriptor = new CacheKeyDescriptor
                {
                    ModuleId = module.Id.ToString(),
                    ModuleName = module.Name,
                    Name = field.GetRawConstantValue()?.ToString() ?? string.Empty,
                    Desc = field.Name
                };
                var descAttr = field.GetCustomAttributes().FirstOrDefault(m =>
                    m.GetType().IsAssignableFrom(typeof(DescriptionAttribute)));
                if (descAttr != null)
                    descriptor.Desc = ((DescriptionAttribute)descAttr).Description;
                list.Add(descriptor);
            }
        }
    }


    /// <summary>
    /// 获取模块设置
    /// </summary>
    /// <param name="moduleId"></param>
    /// <param name="moduleType"></param>
    /// <returns></returns>
    private static ModuleOptions? GetModuleOption(Guid moduleId, ModuleType moduleType)
    {
        switch (moduleType)
        {
            case ModuleType.UserModule:
                return App.GetUserModuleOptions.FirstOrDefault(p => p.Id.Equals(moduleId));
            case ModuleType.FrameworkModule:
            default:
                return InternalApp.FrameworkModuleOptions;
        }
    }
}
