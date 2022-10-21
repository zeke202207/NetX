using NetX.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tools.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditLoggingParam : Pager
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("username")]
        public string? UserName { get; set; }
    }
}
