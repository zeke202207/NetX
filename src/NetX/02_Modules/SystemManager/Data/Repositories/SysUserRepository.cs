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
        var result = await this._freeSql.Select<sys_user, sys_user_role, sys_user_dept, sys_role, sys_dept>()
            .LeftJoin((u, ur, ud, r, d) => u.id == ur.userid)
            .LeftJoin((u, ur, ud, r, d) => u.id == ud.userid)
            .LeftJoin((u, ur, ud, r, d) => ur.roleid == r.id)
            .LeftJoin((u, ur, ud, r, d) => ud.deptid == d.id)
            .Where((u, ur, ud, r, d) => ud.deptid == deptId)
            .WhereIf(!string.IsNullOrWhiteSpace(username), (u, ur, ud, r, d) => u.username == username)
            .WhereIf(!string.IsNullOrWhiteSpace(nickname), (u, ur, ud, r, d) => u.nickname == nickname)
            .Page(currentpage, pagesize)
            .ToListAsync((u, ur, ud, r, d) => new Tuple<sys_user, sys_role, sys_dept>(u, r, d));
        return result;
    }

    public async Task<bool> AddUser(sys_user user, string roleid, string deptid)
    {
        using (var uow = this._freeSql.CreateUnitOfWork())
        {
            var userRep = uow.GetRepository<sys_user>();
            var userRoleRep = uow.GetRepository<sys_user_role>();
            var userDeptRep = uow.GetRepository<sys_user_dept>();
            userRep.UnitOfWork = uow;
            userRoleRep.UnitOfWork = uow;
            userDeptRep.UnitOfWork = uow;
            await userRep.InsertAsync(user); 
            if (!string.IsNullOrWhiteSpace(roleid))
                await userRoleRep.InsertAsync(new sys_user_role() { userid = user.id, roleid = roleid });
            if (!string.IsNullOrWhiteSpace(deptid))
                await userDeptRep.InsertAsync(new sys_user_dept() { userid = user.id, deptid = deptid });
            uow.Commit();
        }
        return true;
    }

    public async Task<bool> UpdateUser(sys_user user, string roleid, string deptid)
    {
        using (var uow = this._freeSql.CreateUnitOfWork())
        {
            var userRep = uow.GetRepository<sys_user>();
            var userRoleRep = uow.GetRepository<sys_user_role>();
            var userDeptRep = uow.GetRepository<sys_user_dept>();
            userRep.UnitOfWork = uow;
            userRoleRep.UnitOfWork = uow;
            userDeptRep.UnitOfWork = uow;
            await userRep.UpdateAsync(user);
            if (!string.IsNullOrWhiteSpace(roleid))
            {
                await userRoleRep.DeleteAsync(p => p.userid.Equals(user.id));
                await userRoleRep.InsertAsync(new sys_user_role() { userid = user.id, roleid = roleid });
            }
            if (!string.IsNullOrWhiteSpace(deptid))
            {
                await userDeptRep.DeleteAsync(p => p.userid.Equals(user.id));
                await userDeptRep.InsertAsync(new sys_user_dept() { userid = user.id, deptid = deptid });
            }
            uow.Commit();
        }
        return true;
    }

    public async Task<bool> RemoveUser(string userId)
    {
        using (var uow = this._freeSql.CreateUnitOfWork())
        {
            var userRep = uow.GetRepository<sys_user>();
            var userRoleRep = uow.GetRepository<sys_user_role>();
            var userDeptRep = uow.GetRepository<sys_user_dept>();
            await userRoleRep.DeleteAsync(p => p.userid.Equals(userId));
            await userDeptRep.DeleteAsync(p => p.userid.Equals(userId));
            await userRep.DeleteAsync(p => p.id.Equals(userId));
            uow.Commit();
        }
        return true;
    }

    public async Task<List<string>> GetPremCodes(string userId)
    {
        var result = await this._freeSql.Select<sys_menu, sys_role_menu, sys_user_role>()
            .LeftJoin((m, rm, ur) => m.id == rm.menuid)
            .LeftJoin((m, rm, ur) => rm.roleid == ur.roleid)
            .Where((m, rm, ur) => ur.userid.Equals(userId) && !string.IsNullOrWhiteSpace(m.permission))
            .ToListAsync((m, rm, ur) => m.permission);
        return result;
    }
}

