using System;
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

        private Dictionary<string, AssemblyLoadContext> LoadContexts { get; } = new Dictionary<string, AssemblyLoadContext>();

        public void Add(string assemblyName, AssemblyLoadContext context)
        {
            LoadContexts.Add(assemblyName, context);
        }

        public IEnumerable<AssemblyLoadContext> All()
        {
            return LoadContexts.Select(p => p.Value);
        }
    }
}
