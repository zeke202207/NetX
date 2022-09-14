using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Swagger;

/// <summary>
/// Action描述特性
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class ApiActionDescriptionAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="description"></param>
    public ApiActionDescriptionAttribute(string description)
    {
        Description = description;
    }

    /// <summary>
    /// action描述信息
    /// </summary>
    public string Description { get; set; }
}
