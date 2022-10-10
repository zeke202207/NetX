namespace NetX.Tools.Models;

/// <summary>
/// 
/// </summary>
public class sys_logging : BaseEntity
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int level { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string eventid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string message { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string exception { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string context { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string state { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string threadid { get; set; }
}
