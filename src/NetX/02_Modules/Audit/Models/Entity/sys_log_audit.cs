using Netx.Ddd.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Audit.Models.Entity
{
    public class sys_log_audit : BaseEntity<string>
    {
        /// <summary>
        /// 调用参数
        /// </summary>
        public string parameters { get; set; }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string browserinfo { get; set; }

        /// <summary>
        /// 客户端信息
        /// </summary>
        public string clientname { get; set; }

        /// <summary>
        /// 客户端ip地址
        /// </summary>
        public string clientipaddress { get; set; }

        /// <summary>
        /// 执行耗时
        /// </summary>
        public int executionduration { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime executiontime { get; set; }

        /// <summary>
        /// 返回内容
        /// </summary>
        public string returnvalue { get; set; }

        /// <summary>
        /// 异常对象
        /// </summary>
        public string exception { get; set; }

        /// <summary>
        /// 方法名
        /// </summary>
        public string methodname { get; set; }

        /// <summary>
        /// 服务名
        /// </summary>
        public string servicename { get; set; }

        /// <summary>
        /// 调用者信息
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 自定义数据
        /// </summary>
        public string customdata { get; set; }

        public string desc { get; set; }
    }
}
