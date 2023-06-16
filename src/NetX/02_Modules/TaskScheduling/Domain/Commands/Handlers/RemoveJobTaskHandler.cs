using Microsoft.EntityFrameworkCore;
using Netx.Ddd.Domain;
using NetX.TaskScheduling.Model;
using NetX.TaskScheduling.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.Domain.Commands.Handlers
{
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
            var jobtrigger = await _uow.GetRepository<sys_jobtask_trigger, string>().FirstOrDefaultAsync(p => p.jobtaskid == request.Id);
            if (null != jobtrigger)
            {
                var trigger = await _uow.GetRepository<sys_trigger, string>().FirstOrDefaultAsync(p => p.Id == jobtrigger.Id);
                if (null != trigger)
                    _uow.GetRepository<sys_trigger, string>().Remove(trigger);
                _uow.GetRepository<sys_jobtask_trigger, string>().Remove(jobtrigger);
            }
            _uow.GetRepository<sys_jobtask, string>().Remove(jobtask);
            return await _uow.SaveChangesAsync();
        }
    }
}
