using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.DatabaseSetup;

/// <summary>
/// 数据库迁移创建表
/// </summary>
public abstract class CreateTableMigration : BaseMigration
{
    /// <summary>
    /// 表名
    /// </summary>
    protected string _tableName = string.Empty;

    /// <summary>
    /// 创建表实例对象
    /// </summary>
    /// <param name="tableName"></param>
    public CreateTableMigration(string tableName)
    {
        _tableName = tableName;
    }

    /// <summary>
    /// 回滚
    /// </summary>
    public override void Down()
    {
        try
        {
            Delete.Table(_tableName);
        }
        catch { }
    }
}
