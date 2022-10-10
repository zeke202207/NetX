using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.LogCollector.Models;

/// <summary>
/// 
/// </summary>
public class LoggingModel
{
    /// <summary>
    /// 
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int Level { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string EventId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Exception { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Context { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string State { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string ThreadId { get; set; }
}
