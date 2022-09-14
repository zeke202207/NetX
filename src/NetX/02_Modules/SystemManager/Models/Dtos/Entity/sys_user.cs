using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models;

public class sys_user: BaseEntity
{
    public string username { get;set; }

    public string password { get; set; }

    public string nickname { get; set; }

    public string avatar { get; set; }

    public int status { get; set; }

    public string remark { get; set; }
}
