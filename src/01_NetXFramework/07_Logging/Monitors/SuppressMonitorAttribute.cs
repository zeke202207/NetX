using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Logging.Monitors;

/// <summary>
/// 控制跳过日志监视
/// </summary>
/// <remarks>作用于全局 <see cref="LoggingMonitorAttribute"/></remarks>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public sealed class SuppressMonitorAttribute : Attribute
{
}
