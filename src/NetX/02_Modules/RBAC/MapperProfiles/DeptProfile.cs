using AutoMapper;
using NetX.RBAC.Models;

namespace NetX.RBAC.MapperProfiles;

/// <summary>
/// 部门mapper配置清单
/// </summary>
public class DeptProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public DeptProfile()
    {
        ToEntity();
        ToModel();
    }

    private void ToModel()
    {
        CreateMap<sys_dept, DeptModel>();
    }

    private void ToEntity()
    {

    }
}
