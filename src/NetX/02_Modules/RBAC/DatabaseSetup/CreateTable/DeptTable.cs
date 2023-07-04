using FluentMigrator;
using NetX.DatabaseSetup;
using NetX.RBAC.Models;

namespace NetX.RBAC.DatabaseSetup.CreateTable;

/// <summary>
/// 部门表
/// </summary>
[Migration(20091124100602)]
public class DeptTable : CreateTableMigration
{
    /// <summary>
    /// 
    /// </summary>
    public DeptTable()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSDEPT)
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
                   .WithColumn(nameof(sys_dept.Id).ToLower()).AsString(50).PrimaryKey()
                   .WithColumn(nameof(sys_dept.parentid).ToLower()).AsString(50).NotNullable()
                   .WithColumn(nameof(sys_dept.deptname).ToLower()).AsString(255)
                   .WithColumn(nameof(sys_dept.orderno).ToLower()).AsInt32()
                   .WithColumn(nameof(sys_dept.createtime).ToLower()).AsDate().WithDefaultValue(DateTime.Now)
                   .WithColumn(nameof(sys_dept.status).ToLower()).AsInt16().Nullable().WithDefaultValue(1)
                   .WithColumn(nameof(sys_dept.remark).ToLower()).AsString(500).Nullable();
        }
        catch (Exception ex)
        {
        }
    }
}
