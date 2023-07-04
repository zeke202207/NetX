using FluentMigrator;
using NetX.DatabaseSetup;
using NetX.RBAC.Models;

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
                    .WithColumn(nameof(sys_user.Id).ToLower()).AsString(50).PrimaryKey()
                    .WithColumn(nameof(sys_user.username).ToLower()).AsString(50).NotNullable()
                    .WithColumn(nameof(sys_user.password).ToLower()).AsString(50).NotNullable()
                    .WithColumn(nameof(sys_user.nickname).ToLower()).AsString(255).Nullable()
                    .WithColumn(nameof(sys_user.avatar).ToLower()).AsString(500).Nullable()
                    .WithColumn(nameof(sys_user.status).ToLower()).AsInt16().Nullable().WithDefaultValue(1)
                    .WithColumn(nameof(sys_user.email).ToLower()).AsString(255).Nullable()
                    .WithColumn(nameof(sys_user.issystem).ToLower()).AsBoolean().WithDefaultValue(false).WithColumnDescription("是否是系统字段，系统字段将不允许被操作")
                    .WithColumn(nameof(sys_user.remark).ToLower()).AsString(500).Nullable();
            Create.Index("idx_username")
                .OnTable(_tableName)
                .OnColumn("username");
        }
        catch (Exception ex)
        {
        }
    }
}
