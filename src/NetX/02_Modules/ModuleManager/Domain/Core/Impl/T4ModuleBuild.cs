using NetX.ModuleManager.Models;
using NetX.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetX.ModuleManager.Domain
{
    [Scoped]
    public class T4ModuleBuild : IModuleBuild
    {
        private IEnumerable<Type> handlerTypeList;
        private TemplateModel _model;

        public IModuleBuild ProjectConfig(TemplateModel model)
        {
            _model = model;
            if (null == model)
                throw new ArgumentNullException("模板生成模型不能为空");
            if (!Directory.Exists(model.RootPath))
                Directory.CreateDirectory(model.RootPath);
            handlerTypeList = Assembly.GetExecutingAssembly().GetTypes().Where(t =>
                  typeof(ITemplateHandler) != t && typeof(ITemplateHandler).IsAssignableFrom(t));
            return this;
        }

        /// <summary>
        /// 生成模板
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> Build()
        {
            var result = true;
            foreach (var type in handlerTypeList)
            {
                var instance = Activator.CreateInstance(type, _model) as ITemplateHandler;
                if(null == instance)
                    result = false;
                result &= await instance.SaveAsync();
            }
            return true;
        }
    }
}
