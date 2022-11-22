using FreeSql;
using NetX.Common.Attributes;
using NetX.RBAC.Models;

namespace NetX.RBAC.Data.Repositories;

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
    /// <param name="fsql">ORM实例</param>
    public SysMenuRepository(IFreeSql fsql)
        : base(fsql, null, null)
    {
        this._freeSql = fsql;
    }

    /// <summary>
    /// 获取当前登录用户访问菜单集合
    /// </summary>
    /// <param name="userId">用户唯一标识</param>
    /// <returns></returns>
    public async Task<IEnumerable<sys_menu>> GetCurrentUserMenuListAsync(string userId)
    {
        return await this._freeSql.Select<sys_menu, sys_role_menu, sys_user_role, sys_role>()
            .LeftJoin((m, rm, ur, r) => m.id == rm.menuid)
            .LeftJoin((m, rm, ur, r) => rm.roleid == ur.roleid)
            .LeftJoin((m, rm, ur, r) => ur.roleid == r.id)
            .Where((m, rm, ur, r) => ur.userid == userId
            && m.type != 2
            && m.status == (int)Status.Enable
            && r.status == (int)Status.Enable)
            .ToListAsync();
    }

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="menuId">菜单唯一标识</param>
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
                if (uow.Orm.Ado.DataType == DataType.MySql)
                {
                    var menuIds = await menuRep.Select.WithSql($"select id from sys_menu where find_in_set(id,get_child_menu('{menuId}'))")
                    .ToListAsync<string>("id");
                    await roleMenuRep.DeleteAsync(p => menuIds.Contains(p.menuid));
                    await menuRep.DeleteAsync(p => menuIds.Contains(p.id));
                }
                else if(uow.Orm.Ado.DataType == DataType.SqlServer)
                {
                    var menuIds = await menuRep.Select.WithSql(@$"WITH
CTE
AS
(
    SELECT * FROM [sys_menu] WHERE Id='{menuId}'
    UNION ALL
    SELECT G.* FROM CTE INNER JOIN [sys_menu] as G
    ON CTE.id=G.parentid
)
SELECT * FROM CTE ORDER BY id")
                   .ToListAsync<string>("id");
                    await roleMenuRep.DeleteAsync(p => menuIds.Contains(p.menuid));
                    await menuRep.DeleteAsync(p => menuIds.Contains(p.id));
                }
                else
                {
                    throw new NotImplementedException("目前仅实现了mysql递归查询，替换成对应数据库的递归吧");
                }
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
