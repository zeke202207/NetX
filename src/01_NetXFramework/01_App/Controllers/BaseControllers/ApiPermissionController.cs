using NetX.Authentication.Core;

namespace NetX.App;

/// <summary>
/// 授权先控制的api接口提供基类
/// </summary>
[PermissionValidate]
public class ApiPermissionController : BaseController
{

}
