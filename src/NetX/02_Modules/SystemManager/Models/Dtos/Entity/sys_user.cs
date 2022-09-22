namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class sys_user : BaseEntity
{
    /// <summary>
    /// 
    /// </summary>
    public string username { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string password { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string nickname { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string avatar { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? email { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? remark { get; set; }
}
