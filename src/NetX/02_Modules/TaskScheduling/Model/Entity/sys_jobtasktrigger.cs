using Netx.Ddd.Domain.Aggregates;

namespace NetX.TaskScheduling.Model;

public class sys_jobtasktrigger : BaseEntity<string>
{
    public string jobtaskid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime? startat { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime? endat { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool startnow { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int priority { get; set; }

    public string cron { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime createtime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string description { get; set; }

    /// <summary>
    /// 触发器类型
    /// 0：cron
    /// 1：simple
    /// 2：。。。
    /// </summary>
    public int triggertype { get; set; }
}
