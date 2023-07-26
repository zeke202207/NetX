using Netx.Ddd.Core;
using NetX.ModuleManager.Models;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetX.SimpleFileSystem.Model;
using NetX.Module;
using NetX.App;

namespace NetX.ModuleManager.Domain
{
    [Scoped]
    public class ModuleService : IModuleService
    {
        private readonly IQueryBus _cliQuery;
        private readonly ICommandBus _cliCommand;

        public ModuleService(IQueryBus cliQuery, ICommandBus cliCommand)
        {
            _cliQuery = cliQuery;
            _cliCommand = cliCommand;
        }

        /// <summary>
        /// 创建插件模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResultModel> CreateModule(CreateModuleParam model)
        {
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConst.C_APP_TEMP, ModuleManagerConstEnum.C_MODULE_TEMP_PATH, DateTime.Now.ToString("yyyyMMddHHmmss"));
            await this._cliCommand.Send<CreateModuleCommand>(new CreateModuleCommand(rootPath, new ModuleBuildModel()
            {
                Id = Guid.NewGuid().ToString("N"),
                Description = model.Project.Description,
                Enabled = model.Project.Enabled,
                IsSharedAssemblyContext = model.Project.IsSharedAssemblyContext,
                Name = model.Project.Name,
                Alias = model.Project.Alias,
                Version = model.Project.Version,
            }));
            return ZipModule(rootPath).ToSuccessResultModel();
        }

        /// <summary>
        /// 获取全部插件模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ResultModel> GetModules(ModuleParam model)
        {
            return _cliQuery.Send<ModuleQuery,ResultModel>(new ModuleQuery());
        }

        /// <summary>
        /// 打包zip
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string ZipModule(string path)
        {
            var zipFile = $"{path}.zip";
            ZipFile.CreateFromDirectory(path, $"{zipFile}");
            return zipFile;
        }

        /// <summary>
        /// 上传模块
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultModel> UploadModule(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var modelPath = Path.Combine(AppContext.BaseDirectory, ModuleSetupConst.C_MODULE_DIRECTORYNAME);
                var tempFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConst.C_APP_TEMP, ModuleManagerConstEnum.C_MODULE_TEMP_PATH, $"{Guid.NewGuid().ToString("N")}.zip");
                Memory<byte> buffer = new Memory<byte>(new byte[file.Length]);
                await stream.ReadAsync(buffer);
                await File.WriteAllBytesAsync(tempFile, buffer.ToArray());
                ZipFile.ExtractToDirectory(tempFile, modelPath);
                File.Delete(tempFile);
            }
            //TODO:动态加载模块

            return true.ToSuccessResultModel();
        }
    }
}
