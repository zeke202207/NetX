using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.RBAC.DatabaseSetup.CreateTable;

/// <summary>
/// 部门表
/// </summary>
[Migration(2)]
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
        Create.Table(_tableName)
               .WithColumn("id").AsString(50).PrimaryKey()
               .WithColumn("parentid").AsString(50).NotNullable()
               .WithColumn("deptname").AsString(255)
               .WithColumn("orderno").AsInt32()
               .WithColumn("createtime").AsDate().WithDefaultValue(DateTime.Now)
               .WithColumn("status").AsInt16().Nullable().WithDefaultValue(1)
               .WithColumn("remark").AsString(500).Nullable();
    }
}
