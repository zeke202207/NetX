using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Logging.Monitors
{
    /// <summary>
    /// 审计特性
    /// 标记此特性将记录到审计表
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =true, Inherited = true)]
    public class AuditAttribute : Attribute
    {
        /// <summary>
        /// 审计特性实例
        /// </summary>
        public AuditAttribute()
        {

        }
    }
}
