using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Ddd.Domain.Extensions;

/// <summary>
/// 分页Sql扩展
/// </summary>
public static class PagerSqlExtension
{
    /// <summary>
    /// 添加mysql分页sql扩展
    /// </summary>
    /// <param name="sql">查询的sql语句</param>
    /// <param name="currentPage">当前页码</param>
    /// <param name="pageSize">每页大小</param>
    /// <returns>待分页的查询sql语句</returns>
    public static string AppendMysqlPagerSql(this string sql, int currentPage, int pageSize)
    {
        if (string.IsNullOrWhiteSpace(sql))
            throw new ArgumentNullException($"{nameof(sql)}is null");
        if (currentPage <= 0)
            currentPage = 1;
        if (pageSize < 1)
            pageSize = 1;
        return $" limit {(currentPage - 1) * pageSize},{pageSize}";
    }
}
