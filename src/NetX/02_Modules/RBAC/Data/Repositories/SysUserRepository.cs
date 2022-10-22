using FreeSql;
using NetX.Common.Attributes;
using NetX.RBAC.Models;

namespace NetX.RBAC.Data.Repositories;

/// <summary>
/// 用户仓储
/// </summary>
[Scoped]
public class SysUserRepository : BaseRepository<sys_user, string>
{
    private readonly IFreeSql _freeSql;

    /// <summary>
    /// 用户仓储对象实例
    /// </summary>
    /// <param name="fsql"></param>
    public SysUserRepository(IFreeSql fsql)
        : base(fsql, null, null)
    {
        this._freeSql = fsql;
    }

    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <param name="deptId"></param>
    /// <param name="username"></param>
    /// <param name="nickname"></param>
    /// <param name="currentpage"></param>
    /// <param name="pagesize"></param>
    /// <returns></returns>
    public async Task<(IEnumerable<Tuple<sys_user, sys_role, sys_dept>> list,int total)> GetUserListAsync(string deptId, string username, string nickname, int currentpage, int pagesize)
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
        var total = await this._freeSql.Select<sys_user>()
            .WhereIf(!string.IsNullOrWhiteSpace(username), (u) => u.username == username)
            .WhereIf(!string.IsNullOrWhiteSpace(nickname), (u) => u.nickname == nickname)
            .CountAsync();
        return (list: result, total: (int)total);
    }

    /// <summary>
    /// 新增用户信息
    /// </summary>
    /// <param name="user"></param>
    /// <param name="roleid"></param>
    /// <param name="deptid"></param>
    /// <returns></returns>
    public async Task<bool> AddUserAsync(sys_user user, string? roleid, string? deptid)
    {
        bool result = true;
        using (var uow = this._freeSql.CreateUnitOfWork())
        {
            try
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
            catch (Exception ex)
            {
                result = false;
                uow.Rollback();
                throw new Exception("删除角色失败", ex);
            }
        }
        return result;
    }

    /// <summary>
    /// 更新用户信息
    /// </summary>
    /// <param name="user"></param>
    /// <param name="roleid"></param>
    /// <param name="deptid"></param>
    /// <returns></returns>
    public async Task<bool> UpdateUserAsync(sys_user user, string? roleid, string? deptid)
    {
        bool result = true;
        using (var uow = this._freeSql.CreateUnitOfWork())
        {
            try
            {
                var userRep = uow.GetRepository<sys_user>();
                var userRoleRep = uow.GetRepository<sys_user_role>();
                var userDeptRep = uow.GetRepository<sys_user_dept>();
                userRep.UnitOfWork = uow;
                userRoleRep.UnitOfWork = uow;
                userDeptRep.UnitOfWork = uow;
                await userRep.UpdateAsync(user);
                await userRoleRep.DeleteAsync(p => p.userid.Equals(user.id));
                if (!string.IsNullOrWhiteSpace(roleid))
                    await userRoleRep.InsertAsync(new sys_user_role() { userid = user.id, roleid = roleid });
                await userDeptRep.DeleteAsync(p => p.userid.Equals(user.id));
                if (!string.IsNullOrWhiteSpace(deptid))
                    await userDeptRep.InsertAsync(new sys_user_dept() { userid = user.id, deptid = deptid });
                uow.Commit();
            }
            catch (Exception ex)
            {
                result = false;
                uow.Rollback();
                throw new Exception("删除角色失败", ex);
            }
        }
        return result;
    }

    /// <summary>
    /// 删除用户信息
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<bool> RemoveUserAsync(string userId)
    {
        bool result = true;
        using (var uow = this._freeSql.CreateUnitOfWork())
        {
            try
            {
                var userRep = uow.GetRepository<sys_user>();
                var userRoleRep = uow.GetRepository<sys_user_role>();
                var userDeptRep = uow.GetRepository<sys_user_dept>();
                await userRoleRep.DeleteAsync(p => p.userid.Equals(userId));
                await userDeptRep.DeleteAsync(p => p.userid.Equals(userId));
                await userRep.DeleteAsync(p => p.id.Equals(userId));
                uow.Commit();
            }
            catch (Exception ex)
            {
                result = false;
                uow.Rollback();
                throw new Exception("删除角色失败", ex);
            }
        }
        return result;
    }

    /// <summary>
    /// 获取用户权限标识集合
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<string>> GetPremCodesAsync(string userId)
    {
        var result = await this._freeSql.Select<sys_menu, sys_role_menu, sys_user_role>()
            .LeftJoin((m, rm, ur) => m.id == rm.menuid)
            .LeftJoin((m, rm, ur) => rm.roleid == ur.roleid)
            .Where((m, rm, ur) => ur.userid.Equals(userId) && !string.IsNullOrWhiteSpace(m.permission))
            .ToListAsync((m, rm, ur) => m.permission);
        return result;
    }

    /// <summary>
    /// 根据用户id获取api权限认证结果集合
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    public async Task<(bool checkApi, List<string> apis)> GetApiPermCode(string userid)
    {
        var checkApiResult = await this._freeSql.Select<sys_user, sys_user_role, sys_role>()
            .LeftJoin((u, ur, r) => u.id == ur.userid)
            .LeftJoin((u, ur, r) => ur.roleid == r.id)
            .Where((u, ur, r) => u.id.Equals(userid))
            .FirstAsync((u, ur, r) => r);
        if (checkApiResult.apicheck == 0)
            return (checkApi: false, apis: new List<string>());
        var apisResult = await this._freeSql.Select<sys_role, sys_role_api, sys_api>()
            .LeftJoin((r, ra, a) => r.id == ra.roleid)
            .LeftJoin((r, ra, a) => a.id == ra.apiid)
            .Where((r,ra,a)=>r.id.Equals(checkApiResult.id))
            .ToListAsync((r, ra, a) => a.path);
        return (checkApi: true, apis: apisResult);
    }
}

