using NetX.Common.Models;
using NetX.RBAC.Models;

namespace NetX.RBAC.Core;

/// <summary>
/// 账号管理服务接口
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// 登录系统
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<ResultModel<LoginResult>> Login(string username, string password);

    /// <summary>
    /// 获取登录token
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    // Task<string> GetToken(ClaimModel model);

    /// <summary>
    /// 获取登录用户信息
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<ResultModel<UserModel>> GetUserInfo(string? userId);

    /// <summary>
    /// 获取账号列表
    /// </summary>
    /// <param name="userParam"></param>
    /// <returns></returns>
    Task<ResultModel<PagerResultModel<List<UserListModel>>>> GetAccountLists(UserListParam userParam);

    /// <summary>
    /// 判断账号是否存在
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> IsAccountExist(string userName);

    /// <summary>
    /// 添加账号
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> AddAccount(AccountRequestModel model);

    /// <summary>
    /// 更新账号信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> UpdateAccount(AccountRequestModel model);

    /// <summary>
    /// 删除账号信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> RemoveDept(string id);

    /// <summary>
    /// 获取访问权限标识集合
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<ResultModel<IEnumerable<string>>> GetPermCode(string userId);

    /// <summary>
    /// 获取pai访问权限列表
    /// </summary>
    /// <param name="userid"></param>
    /// <returns></returns>
    Task<ResultModel<ApiPermissionModel>> GetApiPermCode(string userid);
}
