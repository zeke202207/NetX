using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.RBAC.DatabaseSetup.CreateTable
{
    /// <summary>
    /// 
    /// </summary>
    [Migration(20091124100609)]
    public class RoleApiTable : CreateTableMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public RoleApiTable()
            : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLEAPI)
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
                       .WithColumn("roleid").AsString(50).PrimaryKey()
                       //.ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLEAPI}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLE}", DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLE, "id")
                       .WithColumn("apiid").AsString(50).PrimaryKey()
                       //.ForeignKey($"fk_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLEAPI}_{DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSAPI}", DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSAPI, "id")
                       ;
            }
            catch (Exception ex)
            {
            }
        }
    }
}
