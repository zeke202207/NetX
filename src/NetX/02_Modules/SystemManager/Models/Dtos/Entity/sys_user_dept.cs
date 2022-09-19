using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models;

public class sys_user_dept
{
    [Column(IsPrimary = true)]
    public string userid { get; set; }
    [Column(IsPrimary = true)]
    public string deptid { get; set; }
}
