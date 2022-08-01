using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX;

internal class CorsAccessorOptions
{
    /// <summary>
    /// 策略名称
    /// </summary>
    public string PolicyName { get; set; } = "NetX.Default.Policy";

    /// <summary>
    /// 预检过期时间
    /// </summary>
    public int PreflightMaxAge { get; set; }
}
