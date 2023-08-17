using Microsoft.EntityFrameworkCore;
using NetX.Common.Attributes;
using NetX.Ddd.Domain;
using NetX.TaskScheduling.Model;

namespace NetX.TaskScheduling.Domain.Commands.Handlers
{
    [Scoped]
    public class RemoveJobTaskHandler : DomainCommandHandler<RemoveJobTaskCommand>
    {
        private readonly IUnitOfWork _uow;

        public RemoveJobTaskHandler(
            IUnitOfWork uow)
        {
            _uow = uow;
        }

        public override async Task<bool> Handle(RemoveJobTaskCommand request, CancellationToken cancellationToken)
        {
            var jobtask = await _uow.GetRepository<sys_jobtask, string>().FirstOrDefaultAsync(p => p.Id == request.Id);
            if (null == jobtask)
                return false;
            var jobtrigger = await _uow.GetRepository<sys_jobtasktrigger, string>().FirstOrDefaultAsync(p => p.jobtaskid == request.Id);
            if (null != jobtrigger)
                _uow.GetRepository<sys_jobtasktrigger, string>().Remove(jobtrigger);
            _uow.GetRepository<sys_jobtask, string>().Remove(jobtask);
            return await _uow.SaveChangesAsync();
        }
    }
}
