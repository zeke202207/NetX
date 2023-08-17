using NetX.Ddd.Domain;
using NetX.TaskScheduling.Model;

namespace NetX.TaskScheduling.Domain
{
    public class JobTaskQueryAll : DomainQuery<IEnumerable<JobTaskModel>>
    {
        public string JobName { get; set; }
        public JobTaskQueryAll(string jobName)
        {
            JobName = jobName;
        }
    }
}
