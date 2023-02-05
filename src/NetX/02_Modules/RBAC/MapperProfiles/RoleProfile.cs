using AutoMapper;
using NetX.RBAC.Models;

namespace NetX.RBAC.MapperProfiles;

/// <summary>
/// 角色mapper配置清单
/// </summary>
public class RoleProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public RoleProfile()
    {
        ToEntity();
        ToModel();
    }

    private void ToEntity()
    {

    }

    private void ToModel()
    {
        CreateMap<(sys_role role, List<string> menuids), RoleModel>()
            .ForMember(dest => dest.Menus, opt => opt.MapFrom(src => src.menuids))
            .ForMember(dest => dest.CheckMenu, opt => opt.MapFrom(src => new CheckMenu() { Checked = src.menuids, HalfChecked = new List<string>() }))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.role.Id))
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.role.rolename))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.role.status))
            .ForMember(dest => dest.ApiCheck, opt => opt.MapFrom(src => src.role.apicheck))
            .ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.role.remark))
            .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.role.createtime));
    }
}
