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
            .Where((m,rm,ur) => ur.userid == userId)
            .ToListAsync();
    }
}
