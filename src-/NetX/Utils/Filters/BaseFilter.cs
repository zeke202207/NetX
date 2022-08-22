using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Utils;

/// <summary>
/// netcore filter 过滤器基类
/// filter 顺序：
/// Authorization Filter  ----授权认证过滤器
/// Resource Filter       ----资源过滤器
/// Action Filter　　     ----Action过滤器
/// Exception Filter      ----异常过滤器
/// Result Filter         ----结果过滤器
/// </summary>
public abstract class BaseFilter
{

}
