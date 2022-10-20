using FreeSql;
using NetX.Logging;
using NetX.Tenants;
using NetX.Tools.Models;
using Newtonsoft.Json.Linq;

namespace NetX.Tools.Core;

/// <summary>
/// 数据库日志记录器
/// </summary>
public class DatabaseLoggingWriter : LoggingBaseService, ILoggingWriter
{
    private readonly IBaseRepository<sys_logging> _logRepository;
    private readonly IBaseRepository<sys_audit_logging> _auditlogRepository;
    private readonly FreeSqlCloud<string>? _freeSqlClould;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logRepository"></param>
    /// <param name="freeSql"></param>
    public DatabaseLoggingWriter(IBaseRepository<sys_logging> logRepository, IBaseRepository<sys_audit_logging> auditlogRepository, IFreeSql freeSql)
    {
        this._logRepository = logRepository;
        this._auditlogRepository = auditlogRepository;
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
            SaveLogging(message);
            if (message.IsWriteToAudit)
                SaveAuditLogging(message);
        }
        catch (Exception ex)
        {
            throw new Exception("日志记录出错", ex);
        }
        finally
        {
            _freeSqlClould?.Change(TenantConst.C_TENANT_DBKEY);
        }
    }

    /// <summary>
    /// 记录系统日志
    /// </summary>
    /// <param name="message"></param>
    private void SaveLogging(LogMessage message)
    {
        try
        {
            var elapsed = 0L;
            var loggingMonitorMsg = message.Context.Get("loggingMonitor")?.ToString();
            if (!string.IsNullOrEmpty(loggingMonitorMsg))
            {
                var loggingMonitoer = Newtonsoft.Json.JsonConvert.DeserializeObject<LoggingMonitor>(loggingMonitorMsg);
                if (null != loggingMonitoer)
                    elapsed = loggingMonitoer.TimeOperationElapsedMilliseconds;
            }
            _logRepository.Insert(new sys_logging()
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
    }

    /// <summary>
    /// 记录审计日志
    /// </summary>
    /// <param name="message"></param>
    private void SaveAuditLogging(LogMessage message)
    {
        try
        {
            if (message.LogLevel != Microsoft.Extensions.Logging.LogLevel.Information)
                return;
            var loggingMonitorMsg = message.Context.Get("loggingMonitor")?.ToString();
            if (string.IsNullOrEmpty(loggingMonitorMsg))
                return;
            var loggingMonitoer = Newtonsoft.Json.JsonConvert.DeserializeObject<LoggingMonitor>(loggingMonitorMsg);
            if (null == loggingMonitoer || loggingMonitoer.HttpMethod.ToUpper() == "GET")
                return;
            this._auditlogRepository.Insert(new sys_audit_logging()
            {
                id = base.CreateId(),
                createtime = message.LogDateTime,
                userid = loggingMonitoer.AuthorizationClaims.FirstOrDefault(p => p.Type.ToLower() == "userid")?.Value,
                username = loggingMonitoer.AuthorizationClaims.FirstOrDefault(p => p.Type.ToLower() == "displayname")?.Value,
                controller = loggingMonitoer.ControllerName,
                action = loggingMonitoer.ActionName,
                detail = loggingMonitorMsg,
                remoteipv4 = loggingMonitoer.RemoteIPv4,
                httpmethod = loggingMonitoer.HttpMethod
            });
        }
        catch (Exception)
        {
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
