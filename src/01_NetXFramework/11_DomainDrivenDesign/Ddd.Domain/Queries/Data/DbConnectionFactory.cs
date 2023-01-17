using Microsoft.Extensions.Options;
using MySqlConnector;
using NetX.Common.Attributes;
using NetX.Tenants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netx.Ddd.Domain;

/// <summary>
/// 数据库链接工厂
/// </summary>
public class DbConnectionFactory
{
    /// <summary>
    /// 创建数据库链接实例
    /// </summary>
    /// <returns></returns>
    public static IDbConnection? CreateDbConnection()
    {
        if (null != TenantContext.CurrentTenant)
        {
            if (TenantContext.CurrentTenant.TenantOption?.DatabaseInfo?.DatabaseType == DatabaseType.MySql)
            {
                var _connection = new MySqlConnection($"{TenantContext.CurrentTenant.TenantOption.DatabaseInfo.ToConnStr()}");
                if (null != _connection && _connection.State != ConnectionState.Open)
                    _connection.Open();
                return _connection;
            }
        }
        return null;
    }
}