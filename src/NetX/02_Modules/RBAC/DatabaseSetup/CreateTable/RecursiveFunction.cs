using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.RBAC.DatabaseSetup.CreateTable;

/// <summary>
/// 
/// </summary>
[Migration(101)]
public class RecursiveFunction : BaseMigration
{
    /// <summary>
    /// 
    /// </summary>
    public RecursiveFunction()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Down()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public override void Up()
    {
        base.IfDatabase("mysql").Execute.EmbeddedScript("NetX.RBAC.DatabaseSetup.CreateTable.get_child_dept.sql");
        base.IfDatabase("mysql").Execute.EmbeddedScript("NetX.RBAC.DatabaseSetup.CreateTable.get_child_menu.sql");
    }
}
