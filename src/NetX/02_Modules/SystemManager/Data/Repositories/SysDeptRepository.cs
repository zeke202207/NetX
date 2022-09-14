using FreeSql;
using NetX.Common.Attributes;
using NetX.SystemManager.Models;
using NetX.SystemManager.Models.Dtos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Data.Repositories;

[Scoped]
public class SysDeptRepository : BaseRepository<sys_dept, string>
{
    public SysDeptRepository(IFreeSql fsql)
        : base(fsql, null, null)
    {
    }
}
