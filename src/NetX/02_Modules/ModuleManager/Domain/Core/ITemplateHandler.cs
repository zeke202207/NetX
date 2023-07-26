using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.ModuleManager.Domain
{
    internal interface ITemplateHandler
    {
        /// <summary>
        /// 文件保存
        /// </summary>
        Task<bool> SaveAsync();
    }
}
