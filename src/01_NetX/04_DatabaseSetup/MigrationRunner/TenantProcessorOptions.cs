using FluentMigrator.Runner.Processors;
using Microsoft.Extensions.Options;
using NetX.Tenants;

namespace NetX.DatabaseSetup;

/// <summary>
/// 
/// </summary>
public class TenantProcessorOptions : IOptionsSnapshot<ProcessorOptions>
{
    private readonly int _commandTimeout = 300;    //5 mins

    /// <summary>
    /// 
    /// </summary>
    public ProcessorOptions Value => new ProcessorOptions()
    {
        ConnectionString = TenantContext.Current.Principal.ConnectionStr,
        Timeout = TimeSpan.FromSeconds(_commandTimeout)
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public ProcessorOptions Get(string name)
    {
        return Value;
    }
}
