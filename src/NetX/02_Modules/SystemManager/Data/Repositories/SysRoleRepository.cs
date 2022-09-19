using FreeSql;
using NetX.Common.Attributes;
using NetX.SystemManager.Models;
using NetX.SystemManager.Models.Dtos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Data.Repositories
{
    [Scoped]
    public class SysRoleRepository : BaseRepository<sys_role, string>
    {
        private readonly IFreeSql _freeSql;

        public SysRoleRepository(IFreeSql fsql)
            : base(fsql, null, null)
        {
            this._freeSql = fsql;
        }

        /// <summary>
        /// 获取role列表和role能访问的菜单列表
        /// menuid仅包含叶子节点
        /// </summary>
        /// <param name="rolename"></param>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public async Task<List<(sys_role role,List<string> menuids)>> GetRoleList(string rolename,int currentpage, int pagesize)
        {
            List<(sys_role role, List<string> menuids)> result = new List<(sys_role role, List<string> menuids)>();
            var roles = this._freeSql.Select<sys_role>()
                .WhereIf(!string.IsNullOrWhiteSpace(rolename), p => p.rolename.Equals(rolename));
            if (currentpage >= 0 && pagesize > 0)
                roles.Page(currentpage, pagesize);
            var roleEntities = await roles.ToListAsync();
            foreach(var roleEntity in roleEntities)
            {
                List<string> list1 = this._freeSql
                    .Select<object>()
                    .WithSql($@"SELECT  m.id FROM sys_role_menu rm
                                    LEFT JOIN sys_menu m ON m.id = rm.menuid
                             WHERE
                                m.id NOT IN (SELECT 
                                        m.parentid
                                    FROM
                                        sys_role_menu rm
                                            LEFT JOIN
                                        sys_menu m ON m.id = rm.menuid)
                                and rm.roleid = '{roleEntity.id}'")
                    .ToList<string>("id");
                result.Add((role: roleEntity, menuids: list1));
            }
            return result;
        }

        public async Task<bool> AddRole(sys_role role, List<string> menuids)
        {
            using (var uow = this._freeSql.CreateUnitOfWork())
            {
                var roleRep = uow.GetRepository<sys_role>();
                var roleMenuRep = uow.GetRepository<sys_role_menu>();
                await roleRep.InsertAsync(role);
                await roleMenuRep.DeleteAsync(p => p.roleid.Equals(role.id));
                if (menuids?.Count > 0)
                    await roleMenuRep.InsertAsync(menuids.ConvertAll<sys_role_menu>(a =>
                    new sys_role_menu()
                    {
                        roleid = role.id,
                        menuid = a
                    }));
                uow.Commit();
            }
            return true;
        }

        public async Task<bool> UpdateRole(sys_role role, List<string> menuids)
        {
            using (var uow = this._freeSql.CreateUnitOfWork())
            {
                var roleRep = uow.GetRepository<sys_role>();
                var roleMenuRep = uow.GetRepository<sys_role_menu>();
                await roleRep.UpdateAsync(role);
                await roleMenuRep.DeleteAsync(p => p.roleid.Equals(role.id));
                if (menuids?.Count > 0)
                    await roleMenuRep.InsertAsync(menuids.ConvertAll<sys_role_menu>(a =>
                    new sys_role_menu()
                    {
                        roleid = role.id,
                        menuid = a
                    }));
                uow.Commit();
            }
            return true;
        }

        public async Task<bool> RemoveRole(string roleId)
        {
            using (var uow = this._freeSql.CreateUnitOfWork())
            {
                var roleRep = uow.GetRepository<sys_role>();
                var roleMenuRep = uow.GetRepository<sys_role_menu>();
                await roleMenuRep.DeleteAsync(p => p.roleid.Equals(roleId));
                await roleRep.DeleteAsync(p => p.id.Equals(roleId));
                uow.Commit();
            }
            return true;
        }
    }
}
