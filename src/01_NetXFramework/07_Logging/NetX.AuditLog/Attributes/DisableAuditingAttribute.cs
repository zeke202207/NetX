using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.AuditLog
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DisableAuditingAttribute : Attribute
    {
    }
}
