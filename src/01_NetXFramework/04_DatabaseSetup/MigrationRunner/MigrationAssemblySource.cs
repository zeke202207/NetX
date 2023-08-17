using FluentMigrator.Runner.Initialization;
using System.Reflection;

namespace NetX.DatabaseSetup;

/// <summary>
/// 
/// </summary>
public class MigrationAssemblySource : IAssemblySource
{
    /// <summary>
    /// 
    /// </summary>
    public IReadOnlyCollection<Assembly> Assemblies => this.Assemblies;
}
