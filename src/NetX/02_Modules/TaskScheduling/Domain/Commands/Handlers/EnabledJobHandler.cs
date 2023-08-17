using Microsoft.EntityFrameworkCore;
using NetX.Common.Attributes;
using NetX.Ddd.Domain;
using NetX.TaskScheduling.Domain.Commands;
using NetX.TaskScheduling.Model;

namespace NetX.TaskScheduling.Domain
{
    [Scoped]
    public class EnabledJobHandler : DomainCommandHandler<EnabledJobCommand>
    {
        private readonly IUnitOfWork _uow;

        public EnabledJobHandler(
            IUnitOfWork uow)
        {
            _uow = uow;
        }

        public override async Task<bool> Handle(EnabledJobCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.GetRepository<sys_jobtask, string>().FirstOrDefaultAsync(p => p.Id.Equals(request.Id));
            if (null == entity || entity.enabled == Convert.ToInt32(request.Enabled))
                return true;
            entity.enabled = Convert.ToInt32(request.Enabled);
            entity.state = 2;
            _uow.GetRepository<sys_jobtask, string>().Update(entity);
            return await _uow.SaveChangesAsync();
        }
    }
}
