using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models;

public class sys_dept : BaseEntity
{
    public string parentid { get; set; }
    public string deptname { get; set; }
    public int orderno { get; set; }
    public DateTime createtime { get; set; }
    public int status { get; set; }
    public string? remark { get; set; }
}
