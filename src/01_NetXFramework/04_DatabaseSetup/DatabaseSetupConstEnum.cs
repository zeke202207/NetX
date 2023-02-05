using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.DatabaseSetup
{

    /// <summary>
    /// code first 数据迁移支持的数据库类型
    /// </summary>
    public enum MigrationSupportDbType
    {
        /// <summary>
        /// 
        /// </summary>
        MySql5,
        /// <summary>
        /// 
        /// </summary>
        SqlServer,
    }

    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DatabaseType
    {
        /// <summary>
        /// 
        /// </summary>
        MySql
    }
}
