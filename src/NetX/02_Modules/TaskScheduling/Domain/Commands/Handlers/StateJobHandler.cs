using Microsoft.EntityFrameworkCore;
using NetX.Common.Attributes;
using NetX.Ddd.Domain;
using NetX.TaskScheduling.Model;

namespace NetX.TaskScheduling.Domain
{
    [Scoped]
    public class StateJobHandler : DomainCommandHandler<StateJobCommand>
    {
        private readonly IUnitOfWork _uow;

        public StateJobHandler(
            IUnitOfWork uow)
        {
            _uow = uow;
        }

        public override async Task<bool> Handle(StateJobCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.GetRepository<sys_jobtask, string>().FirstOrDefaultAsync(p => p.Id.Equals(request.Id));
            if (null == entity)
                return true;
            entity.state = request.State;
            entity.enabled = Convert.ToInt32(true);
            _uow.GetRepository<sys_jobtask, string>().Update(entity);
            return await _uow.SaveChangesAsync();
        }
    }
}
