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
    private readonly MigrationSupportDbType supportDb = MigrationSupportDbType.MySql5;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="supporDbType"></param>
    public TenantSelectingProcessorAccessorOptions( MigrationSupportDbType supporDbType)
    {
        supportDb = supporDbType;
    }

    /// <summary>
    /// 
    /// </summary>
    public SelectingProcessorAccessorOptions Value => new SelectingProcessorAccessorOptions()
    {
        ProcessorId = supportDb.ToString()
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
