using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.DatabaseSetup;

/// <summary>
/// 数据库迁移访问接口
/// </summary>
public interface IMigrationService
{
    /// <summary>
    ///  Executes all found (and unapplied) migrations
    /// </summary>
    Task<bool> MigrateUp();

    Task<bool> MigrateUp(bool checkCache);

    /// <summary>
    ///   Migrate down to the given version
    /// </summary>
    /// <param name="version">需要迁移的版本号</param>
    Task<bool> MigrateDown(long version);
}
