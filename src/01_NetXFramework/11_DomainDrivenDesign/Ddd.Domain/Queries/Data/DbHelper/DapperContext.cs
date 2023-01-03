using Dapper;
using NetX.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netx.Ddd.Domain;

public class DapperContext : IDatabaseContext
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public T ExecuteScalar<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) where T : class
    {
        using(var db = DbConnectionFactory.CreateDbConnection())
        {
            return db.ExecuteScalar<T>(sql,param,null,commandTimeout,commandType);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public T QuerySingle<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) where T : class
    {
        using(var db = DbConnectionFactory.CreateDbConnection())
        {
            return db.QueryFirst<T>(sql, param, null, commandTimeout, commandType);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<T> QueryList<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) where T : class
    {
        using (var db = DbConnectionFactory.CreateDbConnection())
        {
            return db.Query<T>(sql, param, null, false, commandTimeout, commandType);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) where T : class
    {
        using (var db = DbConnectionFactory.CreateDbConnection())
        {
            return await db.ExecuteScalarAsync<T>(sql, param, null, commandTimeout, commandType);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<T> QuerySingleAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) where T : class
    {
        using (var db = DbConnectionFactory.CreateDbConnection())
        {
            return await db.QueryFirstAsync<T>(sql, param, null, commandTimeout, commandType);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<T>> QueryListAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) where T : class
    {
        using (var db = DbConnectionFactory.CreateDbConnection())
        {
            return await db.QueryAsync<T>(sql, param, null, commandTimeout, commandType);
        }
    }
}
