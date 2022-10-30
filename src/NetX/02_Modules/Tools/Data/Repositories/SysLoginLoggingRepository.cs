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
/// 登录日志仓储
/// </summary>
[Scoped]
public class SysLoginLoggingRepository : BaseRepository<sys_login_logging, string>
{
    private readonly IFreeSql _freeSql;

    /// <summary>
    /// 登录日志 仓储对象实例
    /// </summary>
    /// <param name="fsql"></param>
    public SysLoginLoggingRepository(IFreeSql fsql)
        : base(fsql, null, null)
    {
        this._freeSql = fsql;
    }
}
