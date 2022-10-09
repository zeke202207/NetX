using AutoMapper;
using NetX.RBAC.Models;

namespace NetX.RBAC.MapperProfiles;

/// <summary>
/// account 映射清单
/// </summary>
public class AccountProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public AccountProfile()
    {
        ToEntity();
        ToModel();
    }

    private void ToEntity()
    {
        CreateMap<UserModel, sys_user>();
    }

    private void ToModel()
    {
        CreateMap<sys_user, UserModel>();
    }
}
