using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.DatabaseSetup;

public abstract class CreateTableMigration :BaseMigration
{
    protected string _tableName=string.Empty;

    public CreateTableMigration(string tableName)
    {
        _tableName = tableName;
    }

    public override void Down()
    {
        Delete.Table(_tableName);
    }
}
