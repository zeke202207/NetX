using NetX.Authentication.Core;
using NetX.SystemManager.Models;

namespace NetX.SystemManager.Core
{
    public interface IAccountService
    {
        Task<UserModel> Login(string username, string password);

        Task<string> GetToken(ClaimModel model);

        Task<UserModel> GetUserInfo(string userId);

        Task<List<UserListModel>> GetAccountLists(UserListParam userParam);

        Task<bool> IsAccountExist(string userName);

        Task<bool> AddAccount(AccountRequestModel model);

        Task<bool> UpdateAccount(AccountRequestModel model);

        Task<bool> RemoveDept(string id);
    }
}
