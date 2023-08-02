using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.ModuleManager.Models
{
    public class ModuleModel
    {
        public string Id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        /// <example>
        /// NetX.RBAC
        /// </example>
        public string Name { get; set; }

        /// <summary>
        /// 工程别名
        /// </summary>
        /// <example>
        /// RBAC
        /// </example>
        public string Alias { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        /// <example>
        /// 1.0.0.0
        /// </example>
        public string Version { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <example>
        /// true
        /// </example>
        public bool Enabled { get; set; }

        /// <summary>
        ///  是否共享程序集上下文
        /// </summary>
        /// <example>
        /// true
        /// </example>
        public bool IsSharedAssemblyContext { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        /// <example>
        /// at will
        /// </example>
        public string Description { get; set; }

        /// <summary>
        /// 是否已加载
        /// issue：net6 在ServiceCollection builder 之后，不允许在进行注入注册了，暂时没有找到解决方案
        /// 因此，添加一个isloaded属性，用于告诉客户端，在上传模块后，模块注入未成功，需要重启后生效
        /// </summary>
        public bool RestartNeeded { get; set; }
    }
}
