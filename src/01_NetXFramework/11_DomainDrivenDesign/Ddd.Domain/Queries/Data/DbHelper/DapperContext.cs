using Dapper;
using System.Data;

namespace NetX.Ddd.Domain;

/// <summary>
/// Dapper数据库上下文
/// </summary>
public class DapperContext : IDatabaseContext
{
    /// <summary>
    /// Execute parameterized SQL that selects a single value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    public T ExecuteScalar<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        using (var db = DbConnectionFactory.CreateDbConnection())
        {
            return db.ExecuteScalar<T>(sql, param, null, commandTimeout, commandType);
        }
    }

    /// <summary>
    /// Executes a single-row query, returning the data typed as T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    public T QuerySingle<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) where T : class
    {
        using (var db = DbConnectionFactory.CreateDbConnection())
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
    public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
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
    public async Task<IEnumerable<T>> QueryListAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) where T : class
    {
        using (var db = DbConnectionFactory.CreateDbConnection())
        {
            return await db.QueryAsync<T>(sql, param, null, commandTimeout, commandType);
        }
    }
}
