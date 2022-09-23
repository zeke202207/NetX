using AutoMapper;
using NetX.SystemManager.Models;

namespace NetX.SystemManager.MapperProfiles;

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
