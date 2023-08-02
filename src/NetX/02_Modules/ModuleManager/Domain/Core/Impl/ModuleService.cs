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
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis;
using Microsoft.VisualBasic.FileIO;
using NetX.Common;

namespace NetX.ModuleManager.Domain
{
    [Scoped]
    public class ModuleService : IModuleService
    {
        private readonly IQueryBus _cliQuery;
        private readonly ICommandBus _cliCommand;
        private readonly ApplicationPartManager _apm;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public ModuleService(
            IQueryBus cliQuery, 
            ICommandBus cliCommand, 
            ApplicationPartManager apm,
            IWebHostEnvironment env,
            IConfiguration config)
        {
            _cliQuery = cliQuery;
            _cliCommand = cliCommand;
            _apm = apm;
            _env = env;
            _config = config;
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
        /// 上传模块
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultModel> UploadModule(IFormFile file)
        {
            var modulePath = Path.Combine(AppContext.BaseDirectory, ModuleSetupConst.C_MODULE_DIRECTORYNAME);
            var moduleName = await ExtractModuleZip(modulePath,file);
            //TODO:NET6版本，不允许在 app build() 之后再次向 ServiceCollection 注入服务
            //暂时没有找到解决方案，上传module后
            //仅将文件解压到插件目录，重启后生效
            await InjectService(modulePath, moduleName);
            ResetControllActions();
            return true.ToSuccessResultModel();
        }

        /// <summary>
        /// 解压模块文件
        /// </summary>
        /// <param name="modulePath"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task<string> ExtractModuleZip(string modulePath, IFormFile file)
        {
            var tempPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConst.C_APP_TEMP, ModuleManagerConstEnum.C_MODULE_TEMP_PATH);
            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);
            var tempFile = Path.Combine(tempPath, $"{Guid.NewGuid().ToString("N")}.zip");
            var moduleName = "";
            using (var stream = file.OpenReadStream())
            {
                Memory<byte> buffer = new Memory<byte>(new byte[file.Length]);
                await stream.ReadAsync(buffer);
                await File.WriteAllBytesAsync(tempFile, buffer.ToArray());
                ZipFile.ExtractToDirectory(tempFile, modulePath, true);
                //moduleName = ZipFile.OpenRead(tempFile).Entries[0].FullName;
                using(var zip = ZipFile.OpenRead(tempFile))
                {
                    moduleName = zip.Entries[0].FullName;
                }
            }
            File.Delete(tempFile);
            return moduleName;
        }

        /// <summary>
        /// 注入服务
        /// </summary>
        /// <param name="modulePath"></param>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        private async Task InjectService(string modulePath, string moduleName)
        {
            DirectoryInfo di = new DirectoryInfo(Path.Combine(modulePath, moduleName));
            var jsonsFi = di.GetFiles("*.json").FirstOrDefault();
            //动态加载模块
            var builder = new ConfigurationBuilder()
                .SetBasePath(modulePath)
                .AddJsonFile(jsonsFi.FullName)
                .Build();
            var options = builder.Get<ModuleOptions>();
            if (null != options)
            {
                var refDir = Path.Combine(modulePath, Path.GetDirectoryName(jsonsFi.FullName), ModuleSetupConst.C_MODULE_REFDIRECTORYNAME);
                if (Directory.Exists(refDir))
                    Directory.EnumerateFiles(refDir, "*.dll")
                    .ToList().ForEach(p => options.Dependencies.Add(p));
                RunOption.Default.AddMoudleOption(options);
            }
            //TODO:解决运行时注入问题(如何在运行时状态下动态注入新增的module？？？)
            //暂时还没有想到解决方案
            _apm.InjectApplicationPartManager(
                () => App.App.Services, //new ServiceCollection(),//
                options,
                _env,
                _config);
            await Task.CompletedTask;
        }

        /// <summary>
        /// 提供了一种通知Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor的缓存集合无效的方法
        /// </summary>
        private void ResetControllActions()
        {
            AppActionDescriptorChangeProvider.Instance.HasChanged = true;
            AppActionDescriptorChangeProvider.Instance.TokenSource.Cancel();
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

    }
}
