using FreeSql;
using NetX.Common.Attributes;
using NetX.Tools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tools.Data;

/// <summary>
/// 系统审计日志仓储
/// </summary>
[Scoped]
public class SysAuditLoggingRepository : BaseRepository<sys_audit_logging, string>
{
    private readonly IFreeSql _freeSql;

    /// <summary>
    /// logging 仓储对象实例
    /// </summary>
    /// <param name="fsql"></param>
    public SysAuditLoggingRepository(IFreeSql fsql)
        : base(fsql, null, null)
    {
        this._freeSql = fsql;
    }
}
