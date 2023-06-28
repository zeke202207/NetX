using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Common
{
    public static class GuidExtensions
    {
        /// <summary>
        /// 判断Guid是否为空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Guid s)
        {
            return s == Guid.Empty;
        }

        /// <summary>
        /// 判断Guid是否不为空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool NotEmpty(this Guid s)
        {
            return s != Guid.Empty;
        }
    }
}
