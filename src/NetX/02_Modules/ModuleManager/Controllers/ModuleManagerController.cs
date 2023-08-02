using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetX.Ddd.Core;
using NetX.Authentication.Core;
using NetX.ModuleManager.Domain;
using NetX.Common.ModuleInfrastructure;
using NetX.Swagger;
using NetX.ModuleManager.Models;
using Microsoft.AspNetCore.Http;
using NetX.App;

namespace NetX.ModuleManager.Controllers
{
    [ApiControllerDescription(ModuleManagerConstEnum.C_MODULEMANAGER_GROUPNAME, Description = "模块脚手架")]
    public class ModuleManagerController : ModuleManagerBaseController
    {
        private readonly IModuleService _moduleService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleService"></param>
        public ModuleManagerController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        /// <summary>
        /// 创建模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ApiActionDescription("创建模块")]
        [NoPermission]
        [HttpPost]
        public async Task<FileResult> CreateModule(CreateModuleParam model)
        {
            var zipFile = await _moduleService.CreateModule(model) as ResultModel<string>;
            return File(await System.IO.File.ReadAllBytesAsync(zipFile.Result), "application/x-zip-compressed", $"{model.Project.Name}.zip");
        }

        /// <summary>
        /// 获取全部插件模块
        /// </summary>
        /// <param name="moduleParam"></param>
        /// <returns></returns>
        [ApiActionDescription("获取系统全部模块插件")]
        [NoPermission]
        [HttpPost]
        public async Task<ResultModel> Modules(ModuleParam moduleParam)
        {
            return await _moduleService.GetModules(moduleParam);
        }

        /// <summary>
        /// 上传模块插件
        /// </summary>
        /// <param name="file">module zip file</param>
        /// <returns></returns>
        [ApiActionDescription("上传模块插件")]
        [NoPermission]
        [HttpPost]
        public async Task<ResultModel> UploadModule(IFormFile file)
        {
            return await _moduleService.UploadModule(file);
        }
    }
}
