using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.Audit.DatabaseSetup.InitData
{
    [Migration(20230705093102)]
    public class InitApiData : InitDataMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public InitApiData()
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
                Insert.IntoTable(_tableName)
                .Row(new
                {
                    id = "70000000000000000000000000000000",
                    path = "/api/audit/getauditlogs",
                    group = "auditlog",
                    method = "POST",
                    description = "获取审计日志列表",
                    issystem = true,
                });
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Down()
        {
            try
            {
                Execute.Sql($"DELETE FROM {_tableName} WHERE id = '70000000000000000000000000000000'");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
