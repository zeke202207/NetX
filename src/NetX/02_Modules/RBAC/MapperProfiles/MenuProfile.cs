using AutoMapper;
using NetX.RBAC.Models;
using Newtonsoft.Json;

namespace NetX.RBAC.MapperProfiles;

/// <summary>
/// 菜单mapper配置清单
/// </summary>
public class MenuProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public MenuProfile()
    {
        ToEntity();
        ToModel();
    }

    private void ToEntity()
    {
        CreateMap<MenuRequestModel, sys_menu>()
            .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.icon, opt => opt.MapFrom(src => src.Icon))
            .ForMember(dest => dest.isext, opt => opt.MapFrom(src => src.IsExt))
            .ForMember(dest => dest.keepalive, opt => opt.MapFrom(src => src.KeepAlive))
            .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.orderno, opt => opt.MapFrom(src => src.OrderNo))
            .ForMember(dest => dest.permission, opt => opt.MapFrom(src => src.Permission))
            .ForMember(dest => dest.redirect, opt => opt.MapFrom(src => src.Redirect))
            .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.show, opt => opt.MapFrom(src => src.Show));

        CreateMap<MenuRequestModel, MenuMetaData>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.HideChildrenMenu, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.HideBreadcrumb, opt => opt.MapFrom(src => (Status)src.Show == Status.Disabled ? true : false))
            .ForMember(dest => dest.HideMenu, opt => opt.MapFrom(src => (Status)src.Show == Status.Disabled ? true : false))
            .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon))
            .ForMember(dest => dest.KeepAlive, opt => opt.MapFrom(src => src.KeepAlive))
            .ForMember(dest => dest.IgnoreKeepAlive, opt => opt.MapFrom(src => (Status)src.KeepAlive == Status.Disabled ? true : false))
            .ForMember(dest => dest.FrameSrc, opt => opt.MapFrom(src => (Ext)src.IsExt == Ext.Yes ? src.ExtPath : ""));
    }

    private void ToModel()
    {
        CreateMap<sys_menu, MenuModel>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
             .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.parentid))
             .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.path))
             .ForMember(dest => dest.Component, opt => opt.MapFrom(src => src.component))
             .ForMember(dest => dest.Meta, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<MenuMetaData>(src.meta)))
             .ForMember(dest => dest.Title, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<MenuMetaData>(src.meta).Title))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.path))
             .ForMember(dest => dest.Redirect, opt => opt.MapFrom(src => src.redirect))
             .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.createtime))
             .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.icon))
             .ForMember(dest => dest.OrderNo, opt => opt.MapFrom(src => src.orderno))
             .ForMember(dest => dest.Permission, opt => opt.MapFrom(src => src.permission))
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
             .ForMember(dest => dest.Show, opt => opt.MapFrom(src => src.show))
             .ForMember(dest => dest.IsExt, opt => opt.MapFrom(src => src.isext))
             .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.type))
             .ForMember(dest => dest.KeepAlive, opt => opt.MapFrom(src => src.keepalive))
             .ForMember(dest => dest.ExtPath, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<MenuMetaData>(src.meta).FrameSrc));
    }
}
