using FreeSql;
using NetX.Common.Attributes;
using NetX.SystemManager.Models;
using NetX.SystemManager.Models.Dtos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Data;

[Scoped]
public class SysUserRepository : BaseRepository<sys_user, string>
{
    private readonly IFreeSql _freeSql;

    public SysUserRepository(IFreeSql fsql)
        : base(fsql, null, null)
    {
        this._freeSql = fsql;
    }

    public async Task<IEnumerable<Tuple<sys_user, sys_role, sys_dept>>> GetUserList(string deptId,string username,string nickname,int currentpage,int pagesize)
    {
        var resule = await this._freeSql.Select<sys_user, sys_user_role, sys_user_dept, sys_role, sys_dept>()
            .LeftJoin((u, ur, ud, r, d) => u.id == ur.userid)
            .LeftJoin((u, ur, ud, r, d) => u.id == ur.userid)
            .LeftJoin((u, ur, ud, r, d) => ur.roleid == r.id)
            .LeftJoin((u, ur, ud, r, d) => ud.deptid == d.id)
            .Where((u, ur, ud, r, d) => ud.deptid == deptId)
            .WhereIf(!string.IsNullOrWhiteSpace(username), (u, ur, ud, r, d) => u.username == username)
            .WhereIf(!string.IsNullOrWhiteSpace(nickname), (u, ur, ud, r, d) => u.nickname == nickname)
            .Page(currentpage, pagesize)
            .ToListAsync((u, ur, ud, r, d) => new Tuple<sys_user, sys_role, sys_dept>(u, r, d));
        return resule;
    }
}

