using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.Options;

namespace NetX.DatabaseSetup;

/// <summary>
/// 
/// </summary>
public class TenantRunnerOptions : IOptions<RunnerOptions>
{
    /// <summary>
    /// 
    /// </summary>
    public RunnerOptions Value => new RunnerOptions();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public RunnerOptions Get(string name)
    {
        return this.Value;
    }
}
