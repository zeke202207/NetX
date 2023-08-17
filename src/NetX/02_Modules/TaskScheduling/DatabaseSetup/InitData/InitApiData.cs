using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.TaskScheduling.DatabaseSetup.InitData
{
    /// <summary>
    /// 
    /// </summary>
    [Migration(20230628131502)]
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
                    id = "60000000000000000000000000000000",
                    path = "/api/jobtask/addcronjob",
                    group = "jobtask",
                    method = "POST",
                    description = "添加任务",
                    issystem = true,
                })
                .Row(new
                {
                    id = "60000000000000000000000000000001",
                    path = "/api/jobtask/pausejob",
                    group = "jobtask",
                    method = "POST",
                    description = "暂停任务",
                    issystem = true,
                })
                .Row(new
                {
                    id = "60000000000000000000000000000002",
                    path = "/api/jobtask/resumejob",
                    group = "jobtask",
                    method = "POST",
                    description = "恢复任务",
                    issystem = true,
                })
                .Row(new
                {
                    id = "60000000000000000000000000000003",
                    path = "/api/jobtask/removejob",
                    group = "jobtask",
                    method = "DELETE",
                    description = "删除任务",
                    issystem = true,
                })
                .Row(new
                {
                    id = "60000000000000000000000000000004",
                    path = "/api/jobtask/getjoblist",
                    group = "jobtask",
                    method = "POST",
                    description = "获取任务列表",
                    issystem = true,
                })
                .Row(new
                {
                    id = "60000000000000000000000000000005",
                    path = "/api/jobtask/getjobbyid",
                    group = "jobtask",
                    method = "POST",
                    description = "通过id获取任务信息",
                    issystem = true,
                })
                .Row(new
                {
                    id = "60000000000000000000000000000006",
                    path = "/api/jobtask/getjobtasktype",
                    group = "jobtask",
                    method = "POST",
                    description = "获取系统支持的jobtype",
                    issystem = true,
                })
                .Row(new
                {
                    id = "60000000000000000000000000000007",
                    path = "/api/jobtask/enabledjob",
                    group = "jobtask",
                    method = "POST",
                    description = "设置jbo启用禁用状态",
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
                Execute.Sql($"DELETE FROM {_tableName} WHERE id in (" +
                    $"'60000000000000000000000000000000'," +
                    $"'60000000000000000000000000000001'," +
                    $"'60000000000000000000000000000002'," +
                    $"'60000000000000000000000000000003'," +
                    $"'60000000000000000000000000000004'," +
                    $"'60000000000000000000000000000005'," +
                    $"'60000000000000000000000000000006'," +
                    $"'60000000000000000000000000000007')");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
