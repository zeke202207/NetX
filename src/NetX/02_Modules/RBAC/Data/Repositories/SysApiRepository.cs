using FreeSql;
using NetX.Common.Attributes;
using NetX.RBAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Data.Repositories;

/// <summary>
/// api 仓储服务
/// </summary>
[Scoped]
public class SysApiRepository : BaseRepository<sys_api, string>
{
    private readonly IFreeSql _freeSql;

    /// <summary>
    /// api 仓储对象实例
    /// </summary>
    /// <param name="fsql"></param>
    public SysApiRepository(IFreeSql fsql)
        : base(fsql, null, null)
    {
        this._freeSql = fsql;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="apiId"></param>
    /// <returns></returns>
    public async Task<bool> RemoveApi(string apiId)
    {
        bool result = true;
        using (var uow = this._freeSql.CreateUnitOfWork())
        {
            try
            {
                var roleApiRep = uow.GetRepository<sys_role_api>();
                var apiRep = uow.GetRepository<sys_api>();
                await roleApiRep.DeleteAsync(p => p.apiid.Equals(apiId));
                await apiRep.DeleteAsync(p => p.id.Equals(apiId));
                uow.Commit();
            }
            catch (Exception ex)
            {
                result = false;
                uow.Rollback();
                throw new Exception("api菜单失败", ex);
            }
        }
        return result;
    }
}
