using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.DatabaseSetup;

/// <summary>
/// 数据库迁移初始化数据
/// </summary>
public abstract class InitDataMigration : BaseMigration
{
    /// <summary>
    /// 表名
    /// </summary>
    protected string _tableName = string.Empty;

    /// <summary>
    /// 初始化数据实例对象
    /// </summary>
    /// <param name="tableName"></param>
    public InitDataMigration(string tableName)
    {
        _tableName = tableName;
    }
}
