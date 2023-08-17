using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.Ddd.Domain;
using NetX.ModuleManager.Models;

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
                RestartNeeded = !App.App.GetModuleContexts.FirstOrDefault(m => m.Initialize.Key.Equals(p.Id)).IsLoaded
            });
            return Task.FromResult(result.ToSuccessResultModel<IEnumerable<ModuleModel>>());
        }
    }
}
