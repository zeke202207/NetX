using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models.Dtos.Entity
{
    public class sys_role : BaseEntity
    {
        public string rolename { get; set; }
        public int status { get; set; }
        public DateTime createtime { get; set; }
        public string remark { get; set; }
    }
}
