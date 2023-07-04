using FluentMigrator;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NetX.DatabaseSetup;
using NetX.RBAC.Models;

namespace NetX.RBAC.DatabaseSetup.CreateTable;

/// <summary>
/// api资源表
/// </summary>
[Migration(20091124100608)]
public class ApiTable : CreateTableMigration
{
    /// <summary>
    /// 
    /// </summary>
    public ApiTable()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSAPI)
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
                   .WithColumn(nameof(sys_api.Id).ToLower()).AsString(50).PrimaryKey()
                   .WithColumn(nameof(sys_api.path).ToLower()).AsString(255).NotNullable()
                   .WithColumn(nameof(sys_api.group).ToLower()).AsString(255).NotNullable()
                   .WithColumn(nameof(sys_api.method).ToLower()).AsString(50).NotNullable()
                   .WithColumn(nameof(sys_api.issystem).ToLower()).AsBoolean().WithDefaultValue(false).WithColumnDescription("是否是系统字段，系统字段将不允许被操作")
                   .WithColumn(nameof(sys_api.createtime).ToLower()).AsDateTime().WithDefaultValue(DateTime.Now)
                   .WithColumn(nameof(sys_api.description).ToLower()).AsString(500).Nullable();
        }
        catch (Exception ex)
        {
        }
    }
}
