using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models.Dtos.Entity
{
    public class sys_menu : BaseEntity
    {
        public string parentid { get; set; }
        public string icon { get; set; }
        public int type { get; set; }
        public int orderno { get; set; }
        public string permission { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string component { get; set; }
        public string redirect { get; set; }
        public int status { get; set; }
        public int isext { get; set; }
        public int keepalive { get; set; }
        public int show { get; set; }
        public string meta { get; set; }
        public DateTime createtime { get; set; }
    }
}
