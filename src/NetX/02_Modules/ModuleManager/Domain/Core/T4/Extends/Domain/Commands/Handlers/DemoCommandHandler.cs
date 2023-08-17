using NetX.ModuleManager.Models;

namespace NetX.ModuleManager.Domain.Core.T4.src.Domain.Commands.Handlers
{
    public partial class DemoCommandHandler : ITemplateHandler
    {
        private readonly TemplateModel _model;
        private readonly string _directoryName;
        private readonly string _fileName;

        public DemoCommandHandler(TemplateModel model)
        {
            _model = model;
            _directoryName = ModuleManagerConstEnum.C_CLI_SRC;
            _fileName = Path.Combine("Domain", "Commands", "Handlers", $"DemoCommandHandler.cs");
        }

        public async Task<bool> SaveAsync()
        {
            return await TransformText()
                .WriteToFile(
                Path.Combine(_model.RootPath, $"{_model.Project.Alias}_{_directoryName}"),
                _fileName);
        }
    }
}
