using Netx.Ddd.Domain;
using NetX.ModuleManager.Models;
using NetX.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.ModuleManager.Domain
{
    [Scoped]
    public class CreateModuleCommandHandler : DomainCommandHandler<CreateModuleCommand>
    {
        private IModuleBuild _projectBuilder;

        public CreateModuleCommandHandler(IModuleBuild projectBuilder)
        {
            _projectBuilder = projectBuilder;
        }

        public override async Task<bool> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            await _projectBuilder.ProjectConfig(new TemplateModel(request.RootPath, request.projectModel))
                .Build();
            return true;
        }
    }
}
