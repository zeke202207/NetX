using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netx.Ddd.Domain.Extensions;

public static class PagerSqlExtension
{
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
