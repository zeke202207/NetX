using Microsoft.AspNetCore.Mvc;
using NetX.SystemManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Core;

public interface IDeptService
{
    Task<List<DeptModel>> GetDeptList([FromQuery] DeptListParam queryParam);

    Task<bool> AddDept(DeptRequestModel model);

    Task<bool> UpdateDept(DeptRequestModel model);

    Task<bool> RemoveDept(string deptId);
}
