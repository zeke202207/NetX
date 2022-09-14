using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.DatabaseSetup.CreateTable
{
    [Migration(101)]
    public class RecursiveFunction : BaseMigration
    {
        public RecursiveFunction()
        {
        }

        public override void Down()
        {
            
        }

        public override void Up()
        {
            base.IfDatabase("mysql").Execute.EmbeddedScript("NetX.SystemManager.DatabaseSetup.CreateTable.get_child_dept.sql");
            base.IfDatabase("mysql").Execute.EmbeddedScript("NetX.SystemManager.DatabaseSetup.CreateTable.get_child_menu.sql");
        }
    }
}
