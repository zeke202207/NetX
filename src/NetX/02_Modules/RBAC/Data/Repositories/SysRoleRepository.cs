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
    /// <param name="fsql">ORM实例</param>
    public SysRoleRepository(IFreeSql fsql)
        : base(fsql, null, null)
    {
        this._freeSql = fsql;
    }

    /// <summary>
    /// 获取role列表和role能访问的菜单列表
    /// menuid仅包含叶子节点
    /// </summary>
    /// <param name="rolename">角色名称</param>
    /// <param name="status">启用禁用状态</param>
    /// <param name="currentpage">当前页码</param>
    /// <param name="pagesize">每页大小</param>
    /// <returns></returns>
    public async Task<List<(sys_role role, List<string> menuids)>> GetRoleListAsync(string rolename,int? status, int currentpage, int pagesize)
    {
        List<(sys_role role, List<string> menuids)> result = new List<(sys_role role, List<string> menuids)>();
        var roles = this._freeSql.Select<sys_role>()
            .WhereIf(null != status,p=>p.status == status)
            .WhereIf(!string.IsNullOrWhiteSpace(rolename), p => p.rolename.Contains(rolename));
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
    /// <param name="role">角色实体</param>
    /// <param name="menuids">角色拥有的菜单集合</param>
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
    /// <param name="role">角色实体</param>
    /// <param name="menuids">角色拥有的菜单集合</param>
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
    /// <param name="roleId">角色唯一标识</param>
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
    /// 获取后台api鉴权集合
    /// </summary>
    /// <param name="roleId">角色唯一标识</param>
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
    /// 更新后台api鉴权权限
    /// </summary>
    /// <param name="roleId">角色唯一标识</param>
    /// <param name="apiIds">后台可访问api列表</param>
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
