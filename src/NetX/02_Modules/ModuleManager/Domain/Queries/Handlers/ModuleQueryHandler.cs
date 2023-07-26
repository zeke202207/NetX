using NetX.Common.ModuleInfrastructure;
using Netx.Ddd.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetX.ModuleManager.Models;
using NetX.Common.Attributes;

namespace NetX.ModuleManager.Domain
{
    [Scoped]
    public class ModuleQueryHandler : DomainQueryHandler<ModuleQuery, ResultModel>
    {
        public ModuleQueryHandler(IDatabaseContext dbContext) 
            : base(dbContext)
        {
        }

        public override Task<ResultModel> Handle(ModuleQuery request, CancellationToken cancellationToken)
        {
            var result = NetX.App.App.GetUserModuleOptions.Select(p => new ModuleModel()
            {
                Id = p.Id.ToString("N"),
                Name = p.Name,
                Enabled = p.Enabled,
                Description = p.Description,
                IsSharedAssemblyContext = p.IsSharedAssemblyContext,
                Version = p.Version,
            });
            return Task.FromResult(result.ToSuccessResultModel<IEnumerable<ModuleModel>>());
        }
    }
}
