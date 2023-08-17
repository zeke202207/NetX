using NetX.Audit.Models.Entity;
using NetX.Common.Attributes;
using NetX.Ddd.Domain;

namespace NetX.Audit.Domain.Commands.Handlers
{
    [Scoped]
    public class AddAuditHandler : DomainCommandHandler<AddAuditCommand>
    {
        private readonly IUnitOfWork _uow;

        public AddAuditHandler(
            IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<bool> Handle(AddAuditCommand request, CancellationToken cancellationToken)
        {
            await _uow.GetRepository<sys_log_audit, string>().AddAsync(new sys_log_audit()
            {
                browserinfo = request.Info.BrowserInfo,
                clientipaddress = request.Info.ClientIpAddress,
                clientname = request.Info.ClientName,
                customdata = request.Info.CustomData,
                exception = Newtonsoft.Json.JsonConvert.SerializeObject(request.Info.Exception),
                executionduration = request.Info.ExecutionDuration,
                executiontime = request.Info.ExecutionTime,
                Id = request.Info.Id,
                methodname = request.Info.MethodName,
                parameters = request.Info.Parameters,
                returnvalue = request.Info.ReturnValue,
                servicename = request.Info.ServiceName,
                userid = request.Info.UserId,
            });
            bool result = await _uow.SaveChangesAsync(false);
            return result;
        }
    }
}
