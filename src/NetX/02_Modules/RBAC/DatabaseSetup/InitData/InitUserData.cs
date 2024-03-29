﻿using FluentMigrator;
using NetX.DatabaseSetup;

namespace NetX.RBAC.DatabaseSetup.InitData;

/// <summary>
/// 
/// </summary>
[Migration(20091125100604)]
public class InitUserData : InitDataMigration
{
    /// <summary>
    /// 
    /// </summary>
    public InitUserData()
        : base(DatabaseSetupConst.C_DATABASESETUP_TABLENAME_SYSUSER)
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
                    id = "00000000000000000000000000000001",
                    username = "zeke",
                    password = "E10ADC3949BA59ABBE56E057F20F883E",
                    nickname = "zeke",
                    avatar = "images/2020/03/07/1H3A4471-7-150x150.jpg",
                    status = 1,
                    remark = "super admin",
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
            Execute.Sql($"delete from {_tableName}");
        }
        catch (Exception ex)
        {
        }
    }
}
