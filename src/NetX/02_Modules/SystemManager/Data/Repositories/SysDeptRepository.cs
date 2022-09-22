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
    /// <summary>
    /// 部门仓储对象实例
    /// </summary>
    /// <param name="fsql"></param>
    public SysDeptRepository(IFreeSql fsql)
        : base(fsql, null, null)
    {
    }
}
