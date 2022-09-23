using FreeSql;
using NetX.Common.Attributes;
using NetX.SystemManager.Models;

namespace NetX.SystemManager.Data.Repositories;

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
    /// <param name="fsql"></param>
    public SysDeptRepository(IFreeSql fsql)
        : base(fsql, null, null)
    {
        this._freeSql = fsql;
    }

    /// <summary>
    /// 删除部门
    /// </summary>
    /// <param name="deptId"></param>
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
                var deptIds = await deptRep.Select.WithSql($"select id from sys_dept where find_in_set(id,get_child_dept('{deptId}'))")
                    .ToListAsync<string>("id");
                await userDeptRep.DeleteAsync(p => deptIds.Contains(p.deptid));
                await deptRep.DeleteAsync(p => deptIds.Contains(p.id));
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
