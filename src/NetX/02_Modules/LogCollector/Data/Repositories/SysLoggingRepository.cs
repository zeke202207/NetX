using FreeSql;
using NetX.Common.Attributes;
using NetX.LogCollector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.LogCollector.Data.Repositories
{
    /// <summary>
    /// 系统日志仓储
    /// </summary>
    [Scoped]
    public class SysLoggingRepository : BaseRepository<sys_logging, string>
    {
        private readonly IFreeSql _freeSql;

        /// <summary>
        /// logging 仓储对象实例
        /// </summary>
        /// <param name="fsql"></param>
        public SysLoggingRepository(IFreeSql fsql)
            : base(fsql, null, null)
        {
            this._freeSql = fsql;
        }
    }
}
