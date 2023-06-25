using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.Model
{
    public class EnabledJobRequest
    {
        public string Id { get; set; }

        public bool Enabled { get; set; }
    }
}
