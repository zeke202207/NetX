using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.MemoryQueue
{
    public delegate object ServiceFactory(Type serviceType);

    public static class ServiceFactoryExtensions
    {
        public static T GetService<T>(this ServiceFactory factory)
        {
            return (T)factory(typeof(T));
        }

        public static IEnumerable<T> GetServices<T>(this ServiceFactory factory)
        {
            return (IEnumerable<T>)factory(typeof(IEnumerable<T>));
        }
    }
}
