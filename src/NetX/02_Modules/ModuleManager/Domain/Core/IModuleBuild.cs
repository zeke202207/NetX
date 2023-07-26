using NetX.ModuleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.ModuleManager.Domain
{
    /// <summary>
    /// 项目生成器
    /// </summary>
    public interface IModuleBuild
    {
        /// <summary>
        /// 配置项目生成器
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IModuleBuild ProjectConfig(TemplateModel model);

        /// <summary>
        /// 生成
        /// </summary>
        Task<bool> Build();
    }
}
