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
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    /// <returns></returns>
    Task<ResultModel<LoginResult>> Login(string username, string password);

    /// <summary>
    /// 获取登录用户信息
    /// </summary>
    /// <param name="userId">用户id</param>
    /// <returns></returns>
    Task<ResultModel<UserModel>> GetUserInfo(string userId);

    /// <summary>
    /// 获取账号列表
    /// </summary>
    /// <param name="queryParam">用户列表查询条件实体</param>
    /// <returns></returns>
    Task<ResultModel<PagerResultModel<List<UserListModel>>>> GetAccountLists(UserListParam queryParam);

    /// <summary>
    /// 判断账号是否存在
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <returns></returns>
    Task<ResultModel<bool>> IsAccountExist(string userName);

    /// <summary>
    /// 添加账号
    /// </summary>
    /// <param name="model">账号实体对象</param>
    /// <returns></returns>
    Task<ResultModel<bool>> AddAccount(AccountRequestModel model);

    /// <summary>
    /// 更新账号信息
    /// </summary>
    /// <param name="model">账号实体对象</param>
    /// <returns></returns>
    Task<ResultModel<bool>> UpdateAccount(AccountRequestModel model);

    /// <summary>
    /// 删除账号信息
    /// </summary>
    /// <param name="userId">用户唯一标识</param>
    /// <returns></returns>
    Task<ResultModel<bool>> RemoveDept(string userId);

    /// <summary>
    /// 获取访问权限标识集合
    /// </summary>
    /// <param name="userId">用户唯一标识</param>
    /// <returns></returns>
    Task<ResultModel<IEnumerable<string>>> GetPermCode(string userId);

    /// <summary>
    /// 获取pai访问权限列表
    /// </summary>
    /// <param name="userid">用户唯一标识</param>
    /// <returns></returns>
    Task<ResultModel<ApiPermissionModel>> GetApiPermCode(string userid);
}
