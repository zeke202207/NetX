using FluentMigrator.Runner.Initialization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

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
