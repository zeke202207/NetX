using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Models.Entity.EntityCombination
{
    internal class sys_user_roleid_deptid : sys_user
    {
        public string roleid { get; set; }

        public string deptid { get; set; }
    }
}
