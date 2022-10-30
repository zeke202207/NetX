using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.RBAC.DatabaseSetup.CreateTable;

/// <summary>
/// 
/// </summary>
[Migration(3)]
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
        Create.Table(_tableName)
               .WithColumn("id").AsString().PrimaryKey()
               .WithColumn("parentid").AsString(50).NotNullable()
               .WithColumn("icon").AsString(50).Nullable()
               .WithColumn("type").AsInt16().WithDefaultValue(0)
               .WithColumn("orderno").AsInt32()
               .WithColumn("permission").AsString(50).Nullable()
               .WithColumn("createtime").AsDateTime().WithDefaultValue(DateTime.Now)
               .WithColumn("title").AsString(50)
               .WithColumn("path").AsString(255).Nullable()
               .WithColumn("component").AsString(255).Nullable()
               .WithColumn("redirect").AsString(255).Nullable()
               .WithColumn("status").AsInt16().WithDefaultValue(1)
               .WithColumn("isext").AsInt16().WithDefaultValue(0)
               .WithColumn("keepalive").AsInt16().WithDefaultValue(1)
               .WithColumn("show").AsInt16().WithDefaultValue(1)
               .WithColumn("meta").AsString(500).Nullable();
    }
}
