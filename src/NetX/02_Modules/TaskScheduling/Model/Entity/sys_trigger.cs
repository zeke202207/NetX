namespace NetX.TaskScheduling.Model;

public class sys_trigger : BaseEntity
{
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
    /// <summary>
    /// 
    /// </summary>
    public DateTime createtime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string description { get; set; }
}
