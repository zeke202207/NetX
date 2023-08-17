using NetX.Ddd.Domain;
using NetX.TaskScheduling.Model;

namespace NetX.TaskScheduling.Domain
{
    public class JobTaskQueryById : DomainQuery<sys_jobtask>
    {
        public string Id { get; private set; }

        public JobTaskQueryById(string id)
        {
            Id = id;
        }
    }
}
