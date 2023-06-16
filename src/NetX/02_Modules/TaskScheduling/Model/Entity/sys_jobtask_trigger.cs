using Netx.Ddd.Domain;
using Netx.Ddd.Domain.Aggregates;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetX.TaskScheduling.Model.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [UPKey("jobtaskid", "triggerid")]
    public class sys_jobtask_trigger : BaseEntity<string>
    {
        [NotMapped]
        public new string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jobtaskid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string triggerid { get; set; }
    }
}
