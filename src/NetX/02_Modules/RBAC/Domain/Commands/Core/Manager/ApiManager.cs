using Microsoft.EntityFrameworkCore;
using NetX.Common.Attributes;
using NetX.Ddd.Domain;
using NetX.RBAC.Models;

namespace NetX.RBAC.Domain.Commands
{
    [Scoped]
    public class ApiManager : IApiManager
    {
        private readonly IUnitOfWork _uow;
        private readonly IRoleManager _roleManager;

        public ApiManager(IUnitOfWork uow, IRoleManager roleManager)
        {
            _uow = uow;
            _roleManager = roleManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> RemovePermissionCacheAsync(string apiid)
        {
            bool result = true; ;
            var roles = await _uow.GetRepository<sys_role_api, string>().Where(p => p.apiid == apiid).ToListAsync();
            foreach (var role in roles)
            {
                result = result && await _roleManager.RemovePermissionCacheAsync(role.roleid);
            }
            return result;
        }
    }
}
