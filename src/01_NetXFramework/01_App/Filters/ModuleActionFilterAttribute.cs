using Microsoft.AspNetCore.Mvc.Filters;
using NetX.Cache.Core;
using NetX.Module;
using Quartz.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetX.App
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleActionFilterAttribute : ActionFilterAttribute
    {
        private readonly ICacheProvider _cacheProvider;

        public ModuleActionFilterAttribute(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller;
            var classType = controller.GetType().Assembly
                .GetTypes()
                .Where(p => 
                    p.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                    .Where(f => null != f.GetCustomAttribute<ModuleKeyAttribute>()).Count() > 0)
                .FirstOrDefault();
            if (null == classType)
                return;
            var keyFieldInfo = classType.GetFields().Where(p => null != p.GetCustomAttribute<ModuleKeyAttribute>()).FirstOrDefault() ;
            if(null == keyFieldInfo) 
                return;
            var key = keyFieldInfo.GetValue(null)?.ToString();
            if (key.IsNullOrWhiteSpace())
                return;
            string cacheKey = $"{CacheKeys.MODULE_CONTROLLER_MODULEID}{key}";
            ModuleOptions options;
            if (!_cacheProvider.TryGetValue<ModuleOptions>(cacheKey, out options))
            {
                options = InternalApp.UserModeulOptions.FirstOrDefault(p => p.Id.ToString("N").Equals(key));
                if (null != options)
                    _cacheProvider.Set<ModuleOptions>(cacheKey, options);
            }
            if (null!= options && !options.Enabled)
                throw new NotSupportedException("This module is not enabled!");
        }
    }
}
