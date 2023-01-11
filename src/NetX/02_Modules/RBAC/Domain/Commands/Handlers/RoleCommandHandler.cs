using NetX.Common.Attributes;
using Netx.Ddd.Domain;
using NetX.RBAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace NetX.RBAC.Domain;


[Scoped]
public class RoleAddCommandHandler : DomainCommandHandler<RoleAddCommand>
{
    private readonly IUnitOfWork _uow;

    public RoleAddCommandHandler(
        IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<bool> Handle(RoleAddCommand request, CancellationToken cancellationToken)
    {
        var roleEntity = new sys_role()
        {
            Id = Guid.NewGuid().ToString("N"),
            createtime = DateTime.Now,
            remark = request.Remark ?? string.Empty,
            status = int.Parse(request.Status),
            rolename = request.RoleName,
            apicheck = int.Parse(request.ApiCheck)
        };
        await _uow.GetRepository<sys_role, string>().AddAsync(roleEntity);
        var rolemenus = request.Menus.Select(p => new sys_role_menu() { menuid = p, roleid = roleEntity.Id });
        await _uow.GetRepository<sys_role_menu, string>().AddRangeAsync(rolemenus);
        return await _uow.CommitAsync();
    }
}

[Scoped]
public class RoleModifyCommandHandler : DomainCommandHandler<RoleModifyCommand>
{
    private readonly IUnitOfWork _uow;

    public RoleModifyCommandHandler(
        IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<bool> Handle(RoleModifyCommand request, CancellationToken cancellationToken)
    {
        var roleEntity = await _uow.GetRepository<sys_role, string>().FirstOrDefaultAsync(p => p.Id == request.Id);
        if (null == roleEntity)
            throw new RbacException($"没有找到角色信息：{request.Id}", (int)ErrorStatusCode.RoleNotFound);
        roleEntity.remark = request.Remark ?? string.Empty;
        roleEntity.status = int.Parse(request.Status);
        roleEntity.rolename = request.RoleName;
        roleEntity.apicheck = int.Parse(request.ApiCheck);
        _uow.GetRepository<sys_role, string>().Update(roleEntity);
        var rms = await _uow.GetRepository<sys_role_menu, string>().AsQueryable().Where(p => p.roleid == request.Id).ToListAsync();
        if (rms.Any())
            _uow.GetRepository<sys_role_menu, string>().RemoveRange(rms);
        var rolemenus = request.Menus.Select(p => new sys_role_menu() { menuid = p, roleid = roleEntity.Id });
        await _uow.GetRepository<sys_role_menu, string>().AddRangeAsync(rolemenus);
        return await _uow.CommitAsync();
    }
}

[Scoped]
public class RoleRemoveCommandHandler : DomainCommandHandler<RoleRemoveCommand>
{
    private readonly IUnitOfWork _uow;

    public RoleRemoveCommandHandler(
        IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<bool> Handle(RoleRemoveCommand request, CancellationToken cancellationToken)
    {
        var roleEntity = await _uow.GetRepository<sys_role, string>().FirstOrDefaultAsync(p => p.Id == request.Id);
        if (null == roleEntity)
            throw new RbacException($"没有找到角色信息：{request.Id}", (int)ErrorStatusCode.RoleNotFound);
        var rolemenu = await _uow.GetRepository<sys_role_menu, string>().FirstOrDefaultAsync(p => p.roleid == request.Id);
        if (null != rolemenu)
            _uow.GetRepository<sys_role_menu, string>().Remove(rolemenu);
        _uow.GetRepository<sys_role, string>().Remove(roleEntity);
        return await _uow.CommitAsync();

    }
}

[Scoped]
public class RoleStatusModifyCommandHandler : DomainCommandHandler<RoleStatusModifyCommand>
{
    private readonly IUnitOfWork _uow;

    public RoleStatusModifyCommandHandler(
        IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<bool> Handle(RoleStatusModifyCommand request, CancellationToken cancellationToken)
    {
        var roleEntity = await _uow.GetRepository<sys_role, string>().FirstOrDefaultAsync(p => p.Id == request.Id);
        if (null == roleEntity)
            throw new RbacException($"没有找到角色信息：{request.Id}", (int)ErrorStatusCode.RoleNotFound);
        roleEntity.status =  int.Parse(request.Status);
        _uow.GetRepository<sys_role,string>().Update(roleEntity);
        //TODO:更新缓存
        return await _uow.CommitAsync();
    }
}


[Scoped]
public class RoleApiAuthModifyCommandHandler : DomainCommandHandler<RoleApiAuthModifyCommand>
{
    private readonly IUnitOfWork _uow;

    public RoleApiAuthModifyCommandHandler(
        IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<bool> Handle(RoleApiAuthModifyCommand request, CancellationToken cancellationToken)
    {
        var roleEntity = await _uow.GetRepository<sys_role, string>().FirstOrDefaultAsync(p => p.Id == request.Id);
        if (null == roleEntity)
            throw new RbacException($"没有找到角色信息：{request.Id}", (int)ErrorStatusCode.RoleNotFound);
        roleEntity.apicheck = int.Parse(request.Status);
        _uow.GetRepository<sys_role, string>().Update(roleEntity);
        //TODO:更新缓存
        return await _uow.CommitAsync();
    }
}

[Scoped]
public class RoleApiAuthSettingCommandHandler : DomainCommandHandler<RoleApiAuthSettingCommand>
{
    private readonly IUnitOfWork _uow;

    public RoleApiAuthSettingCommandHandler(
        IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<bool> Handle(RoleApiAuthSettingCommand request, CancellationToken cancellationToken)
    {
        var roleapis = request.ApiIds.Select(p => new sys_role_api() { roleid = request.RoleId, apiid = p });
        var all = await _uow.GetRepository<sys_role_api,string>().AsQueryable().Where(p=>p.roleid == request.RoleId).ToListAsync();
        if (all.Any())
            _uow.GetRepository<sys_role_api, string>().RemoveRange(all);
        await _uow.GetRepository<sys_role_api, string>().AddRangeAsync(roleapis);
        //TODO:更新缓存
        return await _uow.CommitAsync();
    }
}