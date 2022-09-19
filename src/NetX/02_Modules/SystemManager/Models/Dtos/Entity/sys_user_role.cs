using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models.Dtos.Entity
{
    public class sys_user_role
    {
        [Column(IsPrimary = true)]
        public string userid { get; set; }
        [Column(IsPrimary = true)]
        public string roleid { get; set; }
    }
}
