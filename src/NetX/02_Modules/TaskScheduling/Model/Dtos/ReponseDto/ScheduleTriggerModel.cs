using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.Model
{
    public class ScheduleTriggerModel
    {
        /// <summary>
        /// 触发器名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 触发器描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// cron表达式
        /// </summary>
        public string CronExpression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool StartNow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Priority { get; set; }
    }
}
