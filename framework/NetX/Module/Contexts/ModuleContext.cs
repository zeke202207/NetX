using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Module
{
    /// <summary>
    /// 模块上下文 
    /// </summary>
    public sealed class ModuleContext
    {
        public IConfiguration Configuration { get; set; }

        public ModuleOptions ModuleOptions { get; set; }
    }
}
