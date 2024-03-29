﻿using Microsoft.EntityFrameworkCore;
using NetX.Common.Attributes;
using NetX.Ddd.Domain;
using NetX.TaskScheduling.Model;

namespace NetX.TaskScheduling.Domain.Commands.Handlers
{
    [Scoped]
    public class SchedulerListenerHandler : DomainCommandHandler<SchedulerListenerCommand>
    {
        private readonly IUnitOfWork _uow;

        public SchedulerListenerHandler(
            IUnitOfWork uow)
        {
            _uow = uow;
        }

        public override async Task<bool> Handle(SchedulerListenerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.GetRepository<sys_jobtask, string>().FirstOrDefaultAsync(p => p.name.Equals(request.Name) && p.group.Equals(request.Group));
            if (null == entity || entity.state == (int)request.State)
                return true;
            entity.state = (int)request.State;
            _uow.GetRepository<sys_jobtask, string>().Update(entity);
            return await _uow.SaveChangesAsync();
        }
    }
}
