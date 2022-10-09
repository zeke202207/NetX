using FreeSql;
using NetX.LogCollector.Models;
using NetX.Logging;
using NetX.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.LogCollector.Core;

/// <summary>
/// 数据库日志记录器
/// </summary>
public class DatabaseLoggingWriter : BaseService,ILoggingWriter
{
    private readonly IBaseRepository<sys_logging> _logRepository;
    private readonly FreeSqlCloud<string> _freeSqlClould;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logRepository"></param>
    /// <param name="freeSql"></param>
    public DatabaseLoggingWriter(IBaseRepository<sys_logging> logRepository, IFreeSql freeSql)
    {
        this._logRepository = logRepository;
        this._freeSqlClould = freeSql as FreeSqlCloud<string>;
    }

    /// <summary>
    /// 记录日志
    /// </summary>
    /// <param name="message"></param>
    public void Write(LogMessage message)
    {
        try
        {
            _freeSqlClould?.Change(TenantContext.CurrentTenant.Principal.Tenant.TenantId);
            var v = _logRepository.Insert(new sys_logging()
            {
                id = base.CreateId(),
                context = SerializeObject<LogContext>(message.Context),
                createtime = base.CreateInsertTime(),
                message = message.Message,
                eventid = message.EventId.ToString(),
                exception = SerializeObject<Exception>(message.Exception),
                level = (int)message.LogLevel,
                name = message.LogName,
                state = SerializeObject<object>(message.State),
                threadid = message.ThreadId.ToString()
            });
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            _freeSqlClould?.Change(TenantConst.C_TENANT_DBKEY);
        }
    }
}
