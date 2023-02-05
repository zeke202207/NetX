using AutoMapper;
using NetX.RBAC.Models;

namespace NetX.RBAC.MapperProfiles;

/// <summary>
/// api 映射清单
/// </summary>
public class ApiProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public ApiProfile()
    {
        ToEntity();
        ToModel();
    }

    private void ToEntity()
    {
        CreateMap<ApiModel, sys_api>();
        CreateMap<ApiRequestModel, sys_api>();
    }

    private void ToModel()
    {
        CreateMap<sys_api, ApiModel>();
    }
}
