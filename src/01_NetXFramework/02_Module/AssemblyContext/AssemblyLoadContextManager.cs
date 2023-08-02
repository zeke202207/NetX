using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Module
{
    public class AssemblyLoadContextManager
    {
        private static Lazy<AssemblyLoadContextManager> _instance = new Lazy<AssemblyLoadContextManager>(() => new AssemblyLoadContextManager());

        private AssemblyLoadContextManager() { }

        public static AssemblyLoadContextManager Instance => _instance.Value;

        private ConcurrentDictionary<string, AssemblyLoadContext> LoadContexts { get; } = new();

        public void AddOrUpdate(string assemblyName, AssemblyLoadContext context)
        {
            LoadContexts.AddOrUpdate(assemblyName, k => context, (k, v) => context);
        }

        public IEnumerable<AssemblyLoadContext> All()
        {
            return LoadContexts.Select(p => p.Value);
        }
    }
}
