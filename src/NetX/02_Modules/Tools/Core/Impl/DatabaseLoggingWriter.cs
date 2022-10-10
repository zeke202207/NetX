using FreeSql;
using NetX.Logging;
using NetX.Tenants;
using NetX.Tools.Models;

namespace NetX.Tools.Core;

/// <summary>
/// 数据库日志记录器
/// </summary>
public class DatabaseLoggingWriter : LoggingBaseService, ILoggingWriter
{
    private readonly IBaseRepository<sys_logging> _logRepository;
    private readonly FreeSqlCloud<string>? _freeSqlClould;

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
            if (null == message.Context)
                return;
            var tenantContext = message.Context.Get<TenantContext>(LoggingConst.C_LOGGING_TENANTCONTEXT_KEY);
            if (null == tenantContext
                || null == tenantContext.Principal
                || null == tenantContext.Principal.Tenant
                || string.IsNullOrWhiteSpace(tenantContext.Principal.Tenant.TenantId))
                return;
            _freeSqlClould?.Change(tenantContext.Principal.Tenant.TenantId);
            var v = _logRepository.Insert(new sys_logging()
            {
                id = base.CreateId(),
                threadid = message.ThreadId.ToString(),
                eventid = message.EventId.ToString(),
                name = message.LogName,
                level = (int)message.LogLevel,
                message = message.Message,
                exception = Serialize<Exception>(message.Exception),
                context = Serialize<LogContext>(message.Context),
                state = Serialize<object>(message.State),
                createtime = message.LogDateTime,
            });
        }
        catch (Exception)
        {

        }
        finally
        {
            _freeSqlClould?.Change(TenantConst.C_TENANT_DBKEY);
        }
    }

    /// <summary>
    /// 序列话对象实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    private string Serialize<T>(T data)
    {
        if (null == data)
            return string.Empty;
        return SerializeObject<T>(data);
    }
}
