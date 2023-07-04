using FluentMigrator;
using NetX.DatabaseSetup;
using NetX.RBAC.Models;

namespace NetX.RBAC.DatabaseSetup.CreateTable;

/// <summary>
/// 
/// </summary>
[Migration(20091124100604)]
public class RoleTable : CreateTableMigration
{
    /// <summary>
    /// 
    /// </summary>
    public RoleTable()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLE)
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
                   .WithColumn(nameof(sys_role.Id).ToLower()).AsString(50).PrimaryKey()
                   .WithColumn(nameof(sys_role.rolename).ToLower()).AsString(50).NotNullable()
                   .WithColumn(nameof(sys_role.status).ToLower()).AsInt16().Nullable().WithDefaultValue(1)
                   .WithColumn(nameof(sys_role.apicheck).ToLower()).AsInt16().Nullable().WithDefaultValue(0)
                   .WithColumn(nameof(sys_role.createtime).ToLower()).AsDateTime().WithDefaultValue(DateTime.Now)
                   .WithColumn(nameof(sys_role.issystem).ToLower()).AsBoolean().WithDefaultValue(false).WithColumnDescription("是否是系统字段，系统字段将不允许被操作")
                   .WithColumn(nameof(sys_role.remark).ToLower()).AsString(500).Nullable();
        }
        catch (Exception ex)
        {
        }
    }
}
