using Microsoft.EntityFrameworkCore;
using Netx.Ddd.Domain;
using NetX.Common.Attributes;
using NetX.RBAC.Models;
using Newtonsoft.Json;

namespace NetX.RBAC.Domain;

[Scoped]
public class MenuAddCommandHandler : DomainCommandHandler<MenuAddCommand>
{
    private readonly IUnitOfWork _uow;

    public MenuAddCommandHandler(
        IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<bool> Handle(MenuAddCommand request, CancellationToken cancellationToken)
    {
        MenuMetaData metaData = new MenuMetaData()
        {
            Title = request.Title,
            HideChildrenMenu = false,
            HideBreadcrumb = (Status)request.Show == Status.Disabled ? true : false,
            HideMenu = (Status)request.Show == Status.Disabled ? true : false,
            Icon = request.Icon,
            KeepAlive = (Status)request.KeepAlive == Status.Enable,
            IgnoreKeepAlive = (Status)request.KeepAlive == Status.Disabled ? true : false,
            FrameSrc = (Ext)request.IsExt == Ext.Yes ? request.ExtPath : ""
        };
        var menuType = request.Type.ToMenuType();
        var menuEntity = new sys_menu()
        {
            Id = Guid.NewGuid().ToString("N"),
            createtime = DateTime.Now,
            status = int.Parse(request.Status),
            icon = request.Icon,
            isext = request.IsExt,
            keepalive = request.KeepAlive,
            title = request.Title,
            orderno = request.OrderNo ?? 0,
            permission = request.Permission,
            redirect = request.Redirect,
            type = int.Parse(request.Type),
            show = request.Show,
            component = (Ext)request.IsExt == Ext.Yes ?
            "IFrame" : menuType == MenuType.Dir ?
            "LAYOUT" : string.IsNullOrWhiteSpace(request.Component) ? "" : request.Component,
            parentid = string.IsNullOrWhiteSpace(request.ParentId) ? RBACConst.C_ROOT_ID : request.ParentId,
            path = string.IsNullOrWhiteSpace(request.Path) ?
                    "" : menuType == MenuType.Dir ?
                    request.Path.StartsWith("/") ?
                    request.Path : $"/{request.Path}" : request.Path,
            meta = JsonConvert.SerializeObject(metaData),
        };
        await _uow.GetRepository<sys_menu, string>().AddAsync(menuEntity);
        return await _uow.SaveChangesAsync();
    }
}

[Scoped]
public class MenuModifyCommandHandler : DomainCommandHandler<MenuModifyCommand>
{
    private readonly IUnitOfWork _uow;

    public MenuModifyCommandHandler(
        IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<bool> Handle(MenuModifyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _uow.GetRepository<sys_menu, string>().FirstOrDefaultAsync(p => p.Id == request.Id);
        if (null == entity)
            throw new RbacException($"没有找到菜单信息：{request.Id}", (int)ErrorStatusCode.MenuNotFound);
        MenuMetaData metaData = new MenuMetaData()
        {
            Title = request.Title,
            HideChildrenMenu = false,
            HideBreadcrumb = (Status)request.Show == Status.Disabled ? true : false,
            HideMenu = (Status)request.Show == Status.Disabled ? true : false,
            Icon = request.Icon,
            KeepAlive = (Status)request.KeepAlive == Status.Enable,
            IgnoreKeepAlive = (Status)request.KeepAlive == Status.Disabled ? true : false,
            FrameSrc = (Ext)request.IsExt == Ext.Yes ? request.ExtPath : ""
        };
        var menuType = request.Type.ToMenuType();
        var menuEntity = new sys_menu()
        {
            Id = entity.Id,
            createtime = entity.createtime,
            status = int.Parse(request.Status),
            icon = request.Icon,
            isext = request.IsExt,
            keepalive = request.KeepAlive,
            title = request.Title,
            orderno = request.OrderNo ?? 0,
            permission = request.Permission,
            redirect = request.Redirect,
            type = int.Parse(request.Type),
            show = request.Show,
            component = (Ext)request.IsExt == Ext.Yes ?
            "IFrame" : menuType == MenuType.Dir ?
            "LAYOUT" : string.IsNullOrWhiteSpace(request.Component) ? "" : request.Component,
            parentid = string.IsNullOrWhiteSpace(request.ParentId) ? RBACConst.C_ROOT_ID : request.ParentId,
            path = string.IsNullOrWhiteSpace(request.Path) ?
                    "" : menuType == MenuType.Dir ?
                    request.Path.StartsWith("/") ?
                    request.Path : $"/{request.Path}" : request.Path,
            meta = JsonConvert.SerializeObject(metaData),
        };
        _uow.GetRepository<sys_menu, string>().Update(menuEntity);
        return await _uow.SaveChangesAsync();
    }
}

[Scoped]
public class MenuRemoveCommandHandler : DomainCommandHandler<MenuRemoveCommand>
{
    private readonly IUnitOfWork _uow;

    public MenuRemoveCommandHandler(
        IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<bool> Handle(MenuRemoveCommand request, CancellationToken cancellationToken)
    {
        var menuIds = await _uow.GetRepository<sys_menu, string>().FromSqlRaw($"select * from sys_menu where find_in_set(id,get_child_menu('{request.Id}'))").AsQueryable().Select(p => p.Id).ToListAsync();
        foreach (var menuId in menuIds)
        {
            await _uow.GetRepository<sys_role_menu, string>().FromSqlRaw($"SELECT * FROM sys_role_menu WHERE menuid = '{menuId}'").ExecuteDeleteAsync();
            await _uow.GetRepository<sys_menu, string>().FromSqlRaw($"SELECT * FROM sys_menu where id ='{menuId}'").ExecuteDeleteAsync();
        }
        return await _uow.SaveChangesAsync();
    }
}
