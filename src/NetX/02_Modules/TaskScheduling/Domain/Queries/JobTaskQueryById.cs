using NetX.Common.ModuleInfrastructure;
using NetX.Ddd.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetX.TaskScheduling.Model;
using System.Diagnostics.CodeAnalysis;

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
