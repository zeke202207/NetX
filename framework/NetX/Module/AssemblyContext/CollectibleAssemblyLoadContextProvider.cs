using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Module
{
    /// <summary>
    /// 可回收程序集provider
    /// </summary>
    internal sealed class CollectibleAssemblyLoadContextProvider
    {
        /// <summary>
        /// 获取可回收的程序集
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="mvcBuilder"></param>
        /// <returns></returns>
        public ModuleAssemblyLoadContext Get(ModuleOptions options, IMvcBuilder mvcBuilder)
        {
            return Get(options, mvcBuilder.PartManager);
        }

        /// <summary>
        /// 获取可回收的程序集
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="apm"></param>
        /// <returns></returns>
        public ModuleAssemblyLoadContext Get(ModuleOptions options, ApplicationPartManager apm)
        {
            var modelPath = Path.Combine(AppContext.BaseDirectory, NetXConst.C_MODULE_DIRECTORYNAME);
            var filePath = Path.Combine(modelPath, Path.GetFileNameWithoutExtension(options.FileName), options.FileName);
            var refPath = Path.Combine(modelPath, NetXConst.C_MODULE_REFDIRECTORYNAME);
            ModuleAssemblyLoadContext context = new ModuleAssemblyLoadContext(options.Id.ToString());
            using(var fs = new FileStream(filePath,FileMode.Open))
            {
                //0. 将程序集装在到context中
                var assembly = context.LoadFromStream(fs);
                //1. 将程序集引用装在到context中
                options.Dependencies?.ForEach(p =>
                {
                    using (var fsRef = new FileStream(p, FileMode.Open))
                    {
                        var assembly = context.LoadFromStream(fsRef);
                    }
                });
                //2. apm装在 assemblypart程序集
                AssemblyPart part = new AssemblyPart(assembly);
                apm.ApplicationParts.Add(part);
            }
            return context;
        }
    }
}
