using NetX.Common.Attributes;
using NetX.Ddd.Domain;
using NetX.TaskScheduling.Model;

namespace NetX.TaskScheduling.Domain
{
    [Scoped]
    public class JobTaskQueryByIdHandler : DomainQueryHandler<JobTaskQueryById, sys_jobtask>
    {
        public JobTaskQueryByIdHandler(IDatabaseContext dbContext)
            : base(dbContext)
        {

        }

        public override async Task<sys_jobtask> Handle(JobTaskQueryById request, CancellationToken cancellationToken)
        {
            var jobtask = await base._dbContext.QuerySingleAsync<sys_jobtask>($"SELECT * FROM sys_jobtask WHERE id =@id", new { id = request.Id });
            return jobtask;
        }
    }
}
