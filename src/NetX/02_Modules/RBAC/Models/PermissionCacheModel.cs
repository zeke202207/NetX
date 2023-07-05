using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Models
{
    public sealed class PermissionCacheModel
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 后台是否检查api
        /// </summary>
        public bool CheckApi { get; set; }

        /// <summary>
        /// 权限集合
        /// </summary>
        public List<string> Apis { get; set; }

        /// <summary>
        /// 数据缓存的时间
        /// </summary>
        public DateTime CacheTime { get; }

        public double Duration
        {
            get
            {
                return (DateTime.Now - CacheTime).TotalMilliseconds;
            }
        }

        internal PermissionCacheModel()
        {
            CacheTime = DateTime.Now;
            Apis = new List<string>();
        }
    }
}
