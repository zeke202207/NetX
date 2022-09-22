using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.SystemManager.DatabaseSetup.CreateTable;

/// <summary>
/// 
/// </summary>
[Migration(1)]
public class UserTable : CreateTableMigration
{
    /// <summary>
    /// 
    /// </summary>
    public UserTable()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSER)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Up()
    {
        Create.Table(_tableName)
                .WithColumn("id").AsString().PrimaryKey()
                .WithColumn("username").AsString(50).NotNullable()
                .WithColumn("password").AsString(50).NotNullable()
                .WithColumn("nickname").AsString(255).Nullable()
                .WithColumn("avatar").AsString(500).Nullable()
                .WithColumn("status").AsInt16().Nullable().WithDefaultValue(1)
                .WithColumn("email").AsString(255).Nullable()
                .WithColumn("remark").AsString(500).Nullable();
    }
}
