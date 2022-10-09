using FreeSql;
using NetX.Common.Attributes;
using NetX.RBAC.Models;

namespace NetX.RBAC.Data.Repositories;

/// <summary>
/// 角色仓储
/// </summary>
[Scoped]
public class SysRoleRepository : BaseRepository<sys_role, string>
{
    private readonly IFreeSql _freeSql;

    /// <summary>
    /// 角色仓储对象实例
    /// </summary>
    /// <param name="fsql"></param>
    public SysRoleRepository(IFreeSql fsql)
        : base(fsql, null, null)
    {
        this._freeSql = fsql;
    }

    /// <summary>
    /// 获取role列表和role能访问的菜单列表
    /// menuid仅包含叶子节点
    /// </summary>
    /// <param name="rolename"></param>
    /// <param name="currentpage"></param>
    /// <param name="pagesize"></param>
    /// <returns></returns>
    public async Task<List<(sys_role role, List<string> menuids)>> GetRoleListAsync(string rolename, int currentpage, int pagesize)
    {
        List<(sys_role role, List<string> menuids)> result = new List<(sys_role role, List<string> menuids)>();
        var roles = this._freeSql.Select<sys_role>()
            .WhereIf(!string.IsNullOrWhiteSpace(rolename), p => p.rolename.Equals(rolename));
        if (currentpage >= 0 && pagesize > 0)
            roles.Page(currentpage, pagesize);
        var roleEntities = await roles.ToListAsync();
        foreach (var roleEntity in roleEntities)
        {
            List<string> list1 = this._freeSql
                .Select<object>()
                .WithSql($@"SELECT  m.id FROM sys_role_menu rm
                                    LEFT JOIN sys_menu m ON m.id = rm.menuid
                             WHERE
                                rm.roleid = '{roleEntity.id}'")
                .ToList<string>("id");
            result.Add((role: roleEntity, menuids: list1));
        }
        return result;
    }

    /// <summary>
    /// 新增角色
    /// </summary>
    /// <param name="role"></param>
    /// <param name="menuids"></param>
    /// <returns></returns>
    public async Task<bool> AddRoleAsync(sys_role role, List<string> menuids)
    {
        bool result = true;
        using (var uow = this._freeSql.CreateUnitOfWork())
        {
            try
            {
                var roleRep = uow.GetRepository<sys_role>();
                var roleMenuRep = uow.GetRepository<sys_role_menu>();
                await roleRep.InsertAsync(role);
                await roleMenuRep.DeleteAsync(p => p.roleid.Equals(role.id));
                if (menuids?.Count > 0)
                    await roleMenuRep.InsertAsync(menuids.ConvertAll<sys_role_menu>(a =>
                    new sys_role_menu()
                    {
                        roleid = role.id,
                        menuid = a
                    }));
                uow.Commit();
            }
            catch (Exception ex)
            {
                result = false;
                uow.Rollback();
                throw new Exception("添加角色失败", ex);
            }
        }
        return result;
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    /// <param name="role"></param>
    /// <param name="menuids"></param>
    /// <returns></returns>
    public async Task<bool> UpdateRoleAsync(sys_role role, List<string> menuids)
    {
        bool result = true;
        using (var uow = this._freeSql.CreateUnitOfWork())
        {
            try
            {
                var roleRep = uow.GetRepository<sys_role>();
                var roleMenuRep = uow.GetRepository<sys_role_menu>();
                await roleRep.UpdateAsync(role);
                await roleMenuRep.DeleteAsync(p => p.roleid.Equals(role.id));
                if (menuids?.Count > 0)
                    await roleMenuRep.InsertAsync(menuids.ConvertAll<sys_role_menu>(a =>
                    new sys_role_menu()
                    {
                        roleid = role.id,
                        menuid = a
                    }));
                uow.Commit();
            }
            catch (Exception ex)
            {
                result = false;
                uow.Rollback();
                throw new Exception("更新角色失败", ex);
            }
        }
        return result;
    }

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<bool> RemoveRoleAsync(string roleId)
    {
        bool result = true;
        using (var uow = this._freeSql.CreateUnitOfWork())
        {
            try
            {
                var roleRep = uow.GetRepository<sys_role>();
                var roleMenuRep = uow.GetRepository<sys_role_menu>();
                var roleApiRep = uow.GetRepository<sys_role_api>();
                await roleMenuRep.DeleteAsync(p => p.roleid.Equals(roleId));
                await roleApiRep.DeleteAsync(p => p.roleid.Equals(roleId));
                await roleRep.DeleteAsync(p => p.id.Equals(roleId));
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
    /// 
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<string>> GetApiAuth(string roleId)
    {
        using (var uow = this._freeSql.CreateUnitOfWork())
        {
            var roleApiRep = uow.GetRepository<sys_role_api>();
            var result = await roleApiRep.Select.Where(p => p.roleid.Equals(roleId))
                .ToListAsync(p =>  p.apiid);
            return result;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="apiIds"></param>
    /// <returns></returns>
    public async Task<bool> UpdateRoleApiAuth(string roleId, IEnumerable<string> apiIds)
    {
        bool result = true;
        List<sys_role_api> list = new List<sys_role_api>();
        apiIds.ToList().ForEach(p => list.Add(new sys_role_api() { roleid = roleId, apiid = p }));
        using (var uow = this._freeSql.CreateUnitOfWork())
        {
            try
            {
                var roleApiRep = uow.GetRepository<sys_role_api>();
                await roleApiRep.DeleteAsync(p => p.roleid.Equals(roleId));
                if(list.Count > 0)
                    await roleApiRep.InsertAsync(list);
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
}
