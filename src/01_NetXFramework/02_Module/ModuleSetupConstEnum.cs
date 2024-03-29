﻿namespace NetX.Module;

/// <summary>
/// 
/// </summary>
public class ModuleSetupConst
{
    #region Module

    /// <summary>
    /// server host 模块唯一标识
    /// </summary>
    public static Guid C_SERVERHOST_MODULE_ID = new Guid("00000000000000000000000000000001");

    /// <summary>
    /// 目录名称
    /// </summary>
    public const string C_MODULE_DIRECTORYNAME = "modules";

    /// <summary>
    /// 模块配置文件名称
    /// </summary>
    public const string C_MODULE_CINFIGFILENAME = "plugin.json";

    /// <summary>
    /// Ref引用文件目录
    /// </summary>
    public const string C_MODULE_REFDIRECTORYNAME = "ref";

    /// <summary>
    /// 
    /// </summary>
    public const string C_MODULE_FRAMEWORK_VERSION = "1.0.0.0";

    /// <summary>
    /// 
    /// </summary>
    public const string C_MODULE_FRAMEWORK_NAME = "NetX Framework";

    /// <summary>
    /// 
    /// </summary>
    public const string C_MODULE_FRAMEWORK_DESC = "NetX Framework";

    #endregion

}

/// <summary>
/// 模块类型
/// </summary>
public enum ModuleType : byte
{
    /// <summary>
    /// 系统框架模块
    /// </summary>
    FrameworkModule,
    /// <summary>
    /// 业务模块
    /// </summary>
    UserModule
}
