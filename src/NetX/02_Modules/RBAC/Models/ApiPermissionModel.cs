using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Models
{
    /// <summary>
    /// api访问控制实体对象
    /// </summary>
    public class ApiPermissionModel
    {
        /// <summary>
        /// 是否进行pai验证
        /// </summary>
        public bool CheckApi { get; set; }

        /// <summary>
        /// api权限列表集合
        /// </summary>
        public IEnumerable<string> Apis { get; set; }
    }
}
