using Microsoft.AspNetCore.Http;
using NetX.Common.ModuleInfrastructure;
using NetX.ModuleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.ModuleManager.Domain
{
    public interface IModuleService
    {
        Task<ResultModel> CreateModule(CreateModuleParam model);

        Task<ResultModel> GetModules(ModuleParam model);

        Task<ResultModel> UploadModule(IFormFile file);
    }
}
