using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.Model.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class sys_jobtask_trigger
    {
        /// <summary>
        /// 
        /// </summary>
        [Column(IsPrimary = true)]
        public string jobtaskid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column(IsPrimary = true)]
        public string triggerid { get; set; }
    }
}
