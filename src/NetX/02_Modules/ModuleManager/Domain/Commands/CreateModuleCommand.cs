using NetX.Ddd.Domain;
using NetX.ModuleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.ModuleManager
{
    public record CreateModuleCommand(string RootPath, ModuleBuildModel projectModel) : DomainCommand;
}
