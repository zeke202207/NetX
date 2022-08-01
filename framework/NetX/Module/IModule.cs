using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Module;

/// <summary>
/// 模块初始化器接口
/// </summary>
public interface IModule
{
    /// <summary>
    /// 模块唯一标识
    /// </summary>
    public Guid Key { get; }

    /// <summary>
    /// 模块类型
    /// </summary>
    public ModuleType ModuleType { get; }
}
