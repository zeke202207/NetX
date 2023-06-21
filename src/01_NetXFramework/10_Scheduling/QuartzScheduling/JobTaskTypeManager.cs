using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netx.QuartzScheduling
{
    public class JobTaskTypeManager
    {
        private static Lazy<JobTaskTypeManager> instance = new Lazy<JobTaskTypeManager>(()=> new JobTaskTypeManager());

        private ConcurrentDictionary<string,Type> _jobTypeDic = new ConcurrentDictionary<string,Type>();

        public static JobTaskTypeManager Instance => instance.Value;

        private JobTaskTypeManager() { }

        public void Add(string key, Type value)
        {
            _jobTypeDic.AddOrUpdate(key, value, (k, v) => value);
        }

        public Type Get(string key)
        {
            if (_jobTypeDic.TryGetValue(key, out var value))
                return value;
            return null;
        }

        public IEnumerable<string> GetAll()
        {
            foreach (var item in _jobTypeDic.Keys)
                yield return item;
        }

    }
}
