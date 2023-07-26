using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.ModuleManager.Models
{
    public record TemplateModel(string RootPath, ModuleBuildModel Project);
}
