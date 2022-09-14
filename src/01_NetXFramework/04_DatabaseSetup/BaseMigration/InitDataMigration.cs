using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.DatabaseSetup
{
    public abstract class InitDataMigration : BaseMigration
    {
        protected string _tableName = string.Empty;

        public InitDataMigration(string tableName)
        {
            _tableName = tableName;
        }
    }
}
