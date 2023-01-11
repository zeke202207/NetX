using NetX.RBAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Extensions
{
    public static class MenuExtensions
    {
        public static List<MenuModel> ToTree(this List<MenuModel> menus, string parentId)
        {
            var currentMenus = menus.Where(p => p.ParentId == parentId);
            if (null == currentMenus)
                return new List<MenuModel>();
            foreach (var menu in currentMenus)
            {
                var menuTree = ToTree(menus, menu.Id);
                if (menuTree.Count > 0)
                {
                    menu.Children = new List<MenuModel>();
                    menu.Children.AddRange(menuTree);
                }
            }
            return currentMenus.ToList();
        }
    }
}
