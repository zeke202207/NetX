﻿
namespace NetX.Tenants;

/// <summary>
/// 
/// </summary>
public static class TenantExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static string ToConnStr(this DatabaseInfo model)
    {
        return ToConnStr(model, TenantType.Single, string.Empty);
    }

    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    /// <param name="model"></param>
    /// <param name="type"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public static string ToConnStr(this DatabaseInfo model, TenantType type, string? tenantId)
    {
        if (null == model)
            return string.Empty;
        string? schema = model.ToDatabaseName(type, tenantId);
        switch (model.DatabaseType)
        {
            case DatabaseType.SqlServer:
                return $"Server={model.DatabaseHost};Database={schema};user id={model.UserId};password={model.Password};MultipleActiveResultSets=true";
            case DatabaseType.MySql:
            default:
                return $"server={model.DatabaseHost};port={model.DatabasePort};database={schema};userid={model.UserId};pwd={model.Password};Charset=utf8; SslMode=none;Min pool size=1";
        }
    }

    /// <summary>
    /// 数据库Schema name
    /// </summary>
    /// <param name="model"></param>
    /// <param name="type"></param>
    /// <param name="TenantId"></param>
    /// <returns></returns>
    public static string? ToDatabaseName(this DatabaseInfo model, TenantType type, string? TenantId)
    {
        if (null == model)
            return string.Empty;
        return type == TenantType.Single ? model.DatabaseName : $"{TenantId}-{model.DatabaseName}";
    }

    /// <summary>
    /// 获取创建数据库连接字符串
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static string ToCreateDatabaseConnStr(this DatabaseInfo model)
    {
        switch (model.DatabaseType)
        {
            case DatabaseType.SqlServer:
                return $"Server={model.DatabaseHost};user id={model.UserId};password={model.Password};MultipleActiveResultSets=true";
            case DatabaseType.MySql:
            default:
                return $"Data Source={model.DatabaseHost};port={model.DatabasePort};Persist Security Info=yes;UserId={model.UserId}; PWD={model.Password};Charset=utf8; SslMode=none;Min pool size=1";
        }
    }
}
