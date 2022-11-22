using FreeSql;
using NetX.Common.Attributes;
using NetX.RBAC.Models;

namespace NetX.RBAC.Data.Repositories;

/// <summary>
/// 部门仓储
/// </summary>
[Scoped]
public class SysDeptRepository : BaseRepository<sys_dept, string>
{
    private readonly IFreeSql _freeSql;

    /// <summary>
    /// 部门仓储对象实例
    /// </summary>
    /// <param name="fsql">ORM实例</param>
    public SysDeptRepository(IFreeSql fsql)
        : base(fsql, null, null)
    {
        this._freeSql = fsql;
    }

    /// <summary>
    /// 删除部门
    /// </summary>
    /// <param name="deptId">部门唯一标识</param>
    /// <returns></returns>
    public async Task<bool> RemoveDeptAsync(string deptId)
    {
        bool result = true;
        using (var uow = this._freeSql.CreateUnitOfWork())
        {
            try
            {
                var userDeptRep = uow.GetRepository<sys_user_dept>();
                var deptRep = uow.GetRepository<sys_dept>();
                if (uow.Orm.Ado.DataType == DataType.MySql)
                {
                    var deptIds = await deptRep.Select.WithSql($"select id from sys_dept where find_in_set(id,get_child_dept('{deptId}'))")
                    .ToListAsync<string>("id");
                    await userDeptRep.DeleteAsync(p => deptIds.Contains(p.deptid));
                    await deptRep.DeleteAsync(p => deptIds.Contains(p.id));
                }
                else if (uow.Orm.Ado.DataType == DataType.SqlServer)
                {
                    var deptIds = await deptRep.Select.WithSql(@$"
WITH
CTE
AS
(
    SELECT * FROM [sys_dept] WHERE Id='{deptId}'
    UNION ALL
    SELECT G.* FROM CTE INNER JOIN [sys_dept] as G
    ON CTE.id=G.parentid
)
SELECT * FROM CTE ORDER BY id")
                    .ToListAsync<string>("id");
                    await userDeptRep.DeleteAsync(p => deptIds.Contains(p.deptid));
                    await deptRep.DeleteAsync(p => deptIds.Contains(p.id));
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
                throw new Exception("删除部门失败", ex);
            }
        }
        return result;
    }
}
