using NetX.SystemManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Core
{
    public interface IMenuService
    {
        Task<List<MenuModel>> GetCurrentUserMenuList(string userId);

        Task<List<MenuModel>> GetMenuList(MenuListParam param);

        Task<bool> AddMenu(MenuRequestModel model);

        Task<bool> UpdateMenu(MenuRequestModel model);

        Task<bool> RemoveMenu(string menuId);
    }
}
