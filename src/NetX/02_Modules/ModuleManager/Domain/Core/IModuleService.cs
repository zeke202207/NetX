using Microsoft.AspNetCore.Http;
using NetX.Common.ModuleInfrastructure;
using NetX.ModuleManager.Models;

namespace NetX.ModuleManager.Domain
{
    public interface IModuleService
    {
        Task<ResultModel> CreateModule(CreateModuleParam model);

        Task<ResultModel> GetModules(ModuleParam model);

        Task<ResultModel> UploadModule(IFormFile file);
    }
}
