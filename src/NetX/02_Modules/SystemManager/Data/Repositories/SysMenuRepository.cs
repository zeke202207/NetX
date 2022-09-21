using FreeSql;
using NetX.Common.Attributes;
using NetX.SystemManager.Models;
using NetX.SystemManager.Models.Dtos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Data.Repositories;

[Scoped]
public class SysMenuRepository : BaseRepository<sys_menu, string>
{
    private readonly IFreeSql _freeSql;

    public SysMenuRepository(IFreeSql fsql)
        : base(fsql, null, null)
    {
        this._freeSql = fsql;
    }

    public async Task<IEnumerable<sys_menu>> GetCurrentUserMenuList(string userId)
    {
        return await this._freeSql.Select<sys_menu, sys_role_menu, sys_user_role>()
            .LeftJoin((m, rm, ur) => m.id == rm.menuid)
            .LeftJoin((m, rm, ur) => rm.roleid == ur.roleid)
            .Where((m,rm,ur) => ur.userid == userId && m.type != 2 && m.status == (int)Status.Enable)
            .ToListAsync();
    }

    public async Task<bool> RemoveMenu(List<string> menuIds)
    {
        using (var uow = this._freeSql.CreateUnitOfWork())
        {
            var roleMenuRep = uow.GetRepository<sys_role_menu>();
            var menuRep = uow.GetRepository<sys_menu>();
            await roleMenuRep.DeleteAsync(p => menuIds.Contains(p.menuid));
            await menuRep.DeleteAsync(p => menuIds.Contains(p.id));
            uow.Commit();
        }
        return true;
    }
}
