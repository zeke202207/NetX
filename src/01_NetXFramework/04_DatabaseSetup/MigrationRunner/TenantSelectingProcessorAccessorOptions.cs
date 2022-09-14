using FluentMigrator.Runner.Processors;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetX.DatabaseSetup;

/// <summary>
/// 
/// </summary>
public class TenantSelectingProcessorAccessorOptions : IOptionsSnapshot<SelectingProcessorAccessorOptions>
{
    /// <summary>
    /// 
    /// </summary>
    public SelectingProcessorAccessorOptions Value => new SelectingProcessorAccessorOptions()
    {
        ProcessorId = "MySql5"
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public SelectingProcessorAccessorOptions Get(string name)
    {
        return Value;
    }
}
