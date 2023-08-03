using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.QuartzScheduling
{
    public class JobTaskTypeManager
    {
        private static Lazy<JobTaskTypeManager> instance = new Lazy<JobTaskTypeManager>(()=> new JobTaskTypeManager());

        private ConcurrentDictionary<string, JobTaskTypeModel> _jobTypeDic = new ConcurrentDictionary<string, JobTaskTypeModel>();

        public static JobTaskTypeManager Instance => instance.Value;

        private JobTaskTypeManager() { }

        public void Add(string key, JobTaskTypeModel value)
        {
            _jobTypeDic.AddOrUpdate(key, value, (k, v) => value);
        }

        public JobTaskTypeModel Get(string key)
        {
            if (_jobTypeDic.TryGetValue(key, out var value))
                return value;
            return null;
        }

        public IEnumerable<JobTaskTypeModel> GetAll()
        {
            foreach (var item in _jobTypeDic.Values)
                yield return item;
        }

    }

    public class JobTaskTypeModel
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public Type JobTaskType { get; set; }

        public bool Enabled { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class JobTaskAttribute : Attribute
    {
        public string Id { get; private set; }

        public string Name { get; private set; }

        public JobTaskAttribute(string id, string name )
        {
            Id=id;
            Name=name;
        }
    }
}
