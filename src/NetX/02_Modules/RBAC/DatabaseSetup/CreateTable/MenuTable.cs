using FluentMigrator;
using NetX.DatabaseSetup;
using NetX.RBAC.Models;

namespace NetX.RBAC.DatabaseSetup.CreateTable;

/// <summary>
/// 
/// </summary>
[Migration(20091124100603)]
public class MenuTable : CreateTableMigration
{
    /// <summary>
    /// 
    /// </summary>
    public MenuTable()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSMENU)
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
                   .WithColumn(nameof(sys_menu.Id).ToLower()).AsString(50).PrimaryKey()
                   .WithColumn(nameof(sys_menu.parentid).ToLower()).AsString(50).NotNullable()
                   .WithColumn(nameof(sys_menu.icon).ToLower()).AsString(50).Nullable()
                   .WithColumn(nameof(sys_menu.type).ToLower()).AsInt16().WithDefaultValue(0)
                   .WithColumn(nameof(sys_menu.orderno).ToLower()).AsInt32()
                   .WithColumn(nameof(sys_menu.permission).ToLower()).AsString(50).Nullable()
                   .WithColumn(nameof(sys_menu.createtime).ToLower()).AsDateTime().WithDefaultValue(DateTime.Now)
                   .WithColumn(nameof(sys_menu.title).ToLower()).AsString(50)
                   .WithColumn(nameof(sys_menu.path).ToLower()).AsString(255).Nullable()
                   .WithColumn(nameof(sys_menu.component).ToLower()).AsString(255).Nullable()
                   .WithColumn(nameof(sys_menu.redirect).ToLower()).AsString(255).Nullable()
                   .WithColumn(nameof(sys_menu.status).ToLower()).AsInt16().WithDefaultValue(1)
                   .WithColumn(nameof(sys_menu.isext).ToLower()).AsInt16().WithDefaultValue(0)
                   .WithColumn(nameof(sys_menu.keepalive).ToLower()).AsInt16().WithDefaultValue(1)
                   .WithColumn(nameof(sys_menu.show).ToLower()).AsInt16().WithDefaultValue(1)
                   .WithColumn(nameof(sys_menu.meta).ToLower()).AsString(500).Nullable()
                   .WithColumn(nameof(sys_menu.issystem).ToLower()).AsBoolean().WithDefaultValue(false).WithColumnDescription("是否是系统字段，系统字段将不允许被操作");
        }
        catch (Exception ex)
        {
        }
    }
}
