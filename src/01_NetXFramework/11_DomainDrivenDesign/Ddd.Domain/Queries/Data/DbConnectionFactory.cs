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

public class DbConnectionFactory : IDisposable
{
    private static IDbConnection _connection;

    public static IDbConnection CreateDbConnection()
    {
        if (null != TenantContext.CurrentTenant)
        {
            if (TenantContext.CurrentTenant.TenantOption?.DatabaseInfo?.DatabaseType == DatabaseType.MySql)
                _connection = new MySqlConnection($"{TenantContext.CurrentTenant.TenantOption.DatabaseInfo.ToConnStr()}");
        }
        if(null!= _connection && _connection.State != ConnectionState.Open)
            _connection.Open();
        return _connection;
    }

    public void Dispose()
    {
        _connection?.Close();
        _connection?.Dispose();
    }
}
