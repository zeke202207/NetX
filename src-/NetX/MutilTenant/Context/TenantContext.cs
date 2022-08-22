using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.MutilTenant
{
    /// <summary>
    /// 租户上下文
    /// </summary>
    public class TenantContext
    {
        private static readonly AsyncLocal<TenantContext> _instance;

        static TenantContext()
        {
            _instance = new AsyncLocal<TenantContext>();
        }

        /// <summary>
        /// 当前线程的租户上下文信息
        /// </summary>
        public static TenantContext Current
        {
            get
            {
                TenantContext current;
                if ((current = _instance.Value) == null)
                    current = (_instance.Value = new TenantContext());
                return current;
            }
            private set
            {
                _instance.Value = value;
            }
        }

        /// <summary>
        /// 初始化租户信息
        /// </summary>
        /// <param name="principal"></param>
        public void Init(NetXPrincipal principal)
        {
            Principal = principal;
        }

        /// <summary>
        /// 主体对象
        /// </summary>
        public NetXPrincipal Principal { get; private set; }
    }
}
