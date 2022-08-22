using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSetupTest2
{
    /// <summary>
    /// 建库
    /// </summary>
    [Migration(20220819103400)]
    public class AddTable1 : Migration
    {
        public override void Up()
        {
            Create.Table("Zeke")
               .WithColumn("Id").AsInt64().PrimaryKey().Identity()
               .WithColumn("Text").AsString();
        }

        public override void Down()
        {
            Delete.Table("Zeke");
        }
    }
}
