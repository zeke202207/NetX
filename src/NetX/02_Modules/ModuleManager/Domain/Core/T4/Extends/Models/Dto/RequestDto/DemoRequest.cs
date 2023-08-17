using NetX.ModuleManager.Models;

namespace NetX.ModuleManager.Domain.Core.T4.src.Models.Dto.RequestDto
{
    public partial class DemoRequest : ITemplateHandler
    {
        private readonly TemplateModel _model;
        private readonly string _directoryName;
        private readonly string _fileName;

        public DemoRequest(TemplateModel model)
        {
            _model = model;
            _directoryName = ModuleManagerConstEnum.C_CLI_SRC;
            _fileName = Path.Combine("Models", "Dto", "RequestDto", $"DemoResponse.cs");
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
