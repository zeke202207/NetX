using FreeSql;
using NetX.Common.Attributes;
using NetX.SystemManager.Models;

namespace NetX.SystemManager.Data.Repositories;

/// <summary>
/// 菜单仓储服务
/// </summary>
[Scoped]
public class SysMenuRepository : BaseRepository<sys_menu, string>
{
    private readonly IFreeSql _freeSql;

    /// <summary>
    /// 菜单仓储对象实例
    /// </summary>
    /// <param name="fsql"></param>
    public SysMenuRepository(IFreeSql fsql)
        : base(fsql, null, null)
    {
        this._freeSql = fsql;
    }

    /// <summary>
    /// 获取当前登录用户访问菜单集合
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<sys_menu>> GetCurrentUserMenuListAsync(string userId)
    {
        return await this._freeSql.Select<sys_menu, sys_role_menu, sys_user_role>()
            .LeftJoin((m, rm, ur) => m.id == rm.menuid)
            .LeftJoin((m, rm, ur) => rm.roleid == ur.roleid)
            .Where((m, rm, ur) => ur.userid == userId && m.type != 2 && m.status == (int)Status.Enable)
            .ToListAsync();
    }

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="menuIds"></param>
    /// <returns></returns>
    public async Task<bool> RemoveMenuAsync(string menuId)
    {
        bool result = true;
        using (var uow = this._freeSql.CreateUnitOfWork())
        {
            try
            {
                var roleMenuRep = uow.GetRepository<sys_role_menu>();
                var menuRep = uow.GetRepository<sys_menu>();
                var menuIds = await menuRep.Select.WithSql($"select id from sys_menu where find_in_set(id,get_child_menu('{menuId}'))")
                    .ToListAsync<string>("id");
                await roleMenuRep.DeleteAsync(p => menuIds.Contains(p.menuid));
                await menuRep.DeleteAsync(p => menuIds.Contains(p.id));
                uow.Commit();
            }
            catch (Exception ex)
            {
                result = false;
                uow.Rollback();
                throw new Exception("删除菜单失败", ex);
            }
        }
        return result;
    }
}
