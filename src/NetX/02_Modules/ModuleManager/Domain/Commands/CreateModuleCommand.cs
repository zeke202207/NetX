using NetX.Ddd.Domain;
using NetX.ModuleManager.Models;

namespace NetX.ModuleManager
{
    public record CreateModuleCommand(string RootPath, ModuleBuildModel projectModel) : DomainCommand;
}
