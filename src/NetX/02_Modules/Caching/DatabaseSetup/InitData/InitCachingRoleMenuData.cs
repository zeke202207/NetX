using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.Caching.DatabaseSetup
{

    [Migration(20230706091902)]
    public class InitCachingRoleMenuData : InitDataMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public InitCachingRoleMenuData()
            : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSROLEMENU)
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
                        roleid = "00000000000000000000000000000001",
                        menuid = "00000000000000000000000000000016"
                    })
                    .Row(new
                    {
                        roleid = "00000000000000000000000000000001",
                        menuid = "00000000000000000000000000000017"
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
                Execute.Sql($"delete from {_tableName} WHERE roleid ='00000000000000000000000000000001' AND menuid ='00000000000000000000000000000016'");
                Execute.Sql($"delete from {_tableName} WHERE roleid ='00000000000000000000000000000001' AND menuid ='00000000000000000000000000000017'");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
