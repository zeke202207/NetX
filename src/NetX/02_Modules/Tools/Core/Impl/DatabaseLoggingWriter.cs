using FreeSql;
using IP2Region.Net.XDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
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
    private readonly FreeSqlCloud<string>? _freeSqlClould;
    private readonly ISearcher _searcher;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="freeSql"></param>
    /// <param name="searcher"></param>
    public DatabaseLoggingWriter(IFreeSql freeSql, ISearcher searcher)
    {
        this._freeSqlClould = freeSql as FreeSqlCloud<string>;
        this._searcher = searcher;
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
            var monitorMessage = GetLoggingMonitoerMessage(message);
            if (null == monitorMessage)
                return;
            if (message.IsWriteToAudit)
                SaveAuditLogging(message, monitorMessage);
            if (message.IsWriteToLogin)
                SaveLoginLogging(message, monitorMessage);
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
        var elapsed = 0L;
        var loggingMonitorMsg = message.Context.Get("loggingMonitor")?.ToString();
        if (!string.IsNullOrEmpty(loggingMonitorMsg))
        {
            var loggingMonitoer = Newtonsoft.Json.JsonConvert.DeserializeObject<LoggingMonitor>(loggingMonitorMsg);
            if (null != loggingMonitoer)
                elapsed = loggingMonitoer.TimeOperationElapsedMilliseconds;
        }
        var _logRepository = _freeSqlClould.GetCloudRepository<sys_logging>();
        _logRepository.Insert(new sys_logging()
        {
            id = base.CreateId(),
            threadid = message.ThreadId.ToString(),
            eventid = message.EventId.ToString(),
            name = message.LogName,
            level = (int)message.LogLevel,
            message = message.Message,
            exception = Serialize<Exception>(message.Exception),
            //context = Serialize<LogContext>(message.Context),
            state = Serialize<object>(message.State),
            createtime = message.LogDateTime,
            elapsed = elapsed,
        });
    }

    /// <summary>
    /// 记录审计日志
    /// </summary>
    /// <param name="message"></param>
    /// <param name="monitorMessage"></param>
    private void SaveAuditLogging(LogMessage message, LoggingMonitor monitorMessage)
    {
        var _auditlogRepository = _freeSqlClould.GetCloudRepository<sys_audit_logging>();
        _auditlogRepository.Insert(new sys_audit_logging()
        {
            id = base.CreateId(),
            createtime = message.LogDateTime,
            userid = monitorMessage.AuthorizationClaims.FirstOrDefault(p => p.Type.ToLower() == "userid")?.Value??string.Empty,
            username = monitorMessage.AuthorizationClaims.FirstOrDefault(p => p.Type.ToLower() == "displayname")?.Value??string.Empty,
            controller = monitorMessage.ControllerName,
            action = monitorMessage.ActionName,
            detail = message.Context.Get<string>(LoggingConst.C_LOGGING_MONITOR_KEY),
            remoteipv4 = monitorMessage.RemoteIPv4,
            httpmethod = monitorMessage.HttpMethod,
        });
    }

    /// <summary>
    /// 记录登录日志
    /// </summary>
    /// <param name="message">日志消息实体</param>
    /// <param name="monitorMessage"></param>
    private void SaveLoginLogging(LogMessage message, LoggingMonitor monitorMessage)
    {
        if (null == monitorMessage)
            return;
        var loginResult = message.Context.Get<ObjectResult>(LoggingConst.C_LOGIN_RESULT);
        var objectResult = JObject.FromObject(loginResult.Value).GetValue("result");
        if (null == objectResult)
            return;
        var _loginLoggingRepository = _freeSqlClould.GetCloudRepository<sys_login_logging>();
        var entity = new sys_login_logging()
        {
            id = base.CreateId(),
            createtime = message.LogDateTime,
            userid = objectResult.Value<string>("userid")??string.Empty,
            username = objectResult.Value<string>("username") ?? string.Empty,
            loginip = monitorMessage.RemoteIPv4,
            loginaddress = _searcher.Search(monitorMessage.RemoteIPv4) ?? string.Empty,
        };
        _loginLoggingRepository.Insert(entity);
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message">日志实体</param>
    /// <returns></returns>
    private LoggingMonitor GetLoggingMonitoerMessage(LogMessage message)
    {
        var loggingMonitorMsg = message.Context.Get<string>(LoggingConst.C_LOGGING_MONITOR_KEY);
        if (string.IsNullOrWhiteSpace(loggingMonitorMsg))
            return default(LoggingMonitor);
        return Newtonsoft.Json.JsonConvert.DeserializeObject<LoggingMonitor>(loggingMonitorMsg);
    }
}
