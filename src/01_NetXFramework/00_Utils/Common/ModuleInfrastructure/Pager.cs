using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Common.ModuleInfrastructure;

/// <summary>
/// 
/// </summary>
public class Pager
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("page")]
    public int CurrentPage { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("pageSize")]
    public int PageSize { get; set; }
}
