using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager
{
    internal class SystemManagerConst
    {
        public const string C_ROOT_ID = "00000000000000000000000000000000";
    }

    /// <summary>
    /// 启用状态
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// 禁用
        /// </summary>
        Disabled=0,
        /// <summary>
        /// 启用
        /// </summary>
        Enable=1
    }

    /// <summary>
    /// 外链枚举
    /// </summary>
    public enum Ext
    {
        No = 0, 
        Yes = 1
    }

    public enum MenuType
    {
        Dir = 0,
        Menu,
        Button
    }
}
