using NetX.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Audit
{
    public static class AuditConstEnum
    {
        [ModuleKey]
        public static string C_AUDIT_KEY = "10000000000000000000000000000006";

        /// <summary>
        /// swagger分组名称
        /// </summary>
        public const string C_AUDITLOG_GROUPNAME = "audit";
    }
}
