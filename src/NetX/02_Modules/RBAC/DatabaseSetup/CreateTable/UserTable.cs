using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.RBAC.DatabaseSetup.CreateTable;

/// <summary>
/// 
/// </summary>
[Migration(20091124100601)]
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
        try
        {
            Create.Table(_tableName)
                    .WithColumn("id").AsString(50).PrimaryKey()
                    .WithColumn("username").AsString(50).NotNullable()
                    .WithColumn("password").AsString(50).NotNullable()
                    .WithColumn("nickname").AsString(255).Nullable()
                    .WithColumn("avatar").AsString(500).Nullable()
                    .WithColumn("status").AsInt16().Nullable().WithDefaultValue(1)
                    .WithColumn("email").AsString(255).Nullable()
                    .WithColumn("issystem").AsBoolean().WithDefaultValue(false).WithColumnDescription("是否是系统字段，系统字段将不允许被操作")
                    .WithColumn("remark").AsString(500).Nullable();
            Create.Index("idx_username")
                .OnTable(_tableName)
                .OnColumn("username");
        }
        catch (Exception ex)
        {
        }
    }
}
