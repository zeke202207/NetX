using AutoMapper;
using NetX.RBAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
