using FluentMigrator;
using NetX.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tools.DatabaseSetup.CreateTable
{
    /// <summary>
    /// 登录日志
    /// </summary>
    [Migration(2003)]
    public class LoginLoggingTable : CreateTableMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public LoginLoggingTable()
            : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_LOGIN_LOGGING)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Up()
        {
            Create.Table(_tableName)
              .WithColumn("id").AsString(50).PrimaryKey()
              .WithColumn("userid").AsString(50).Nullable()
              .WithColumn("username").AsString(255).Nullable()
              .WithColumn("loginip").AsString(50).Nullable()
              .WithColumn("loginaddress").AsString(50).Nullable()
              .WithColumn("createtime").AsDateTime().WithDefaultValue(DateTime.Now);
        }
    }
}
