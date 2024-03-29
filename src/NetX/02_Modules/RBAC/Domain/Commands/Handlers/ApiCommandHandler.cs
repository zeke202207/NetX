﻿using Microsoft.EntityFrameworkCore;
using NetX.Common.Attributes;
using NetX.Ddd.Domain;
using NetX.RBAC.Domain.Commands;
using NetX.RBAC.Models;

namespace NetX.RBAC.Domain;

[Scoped]
public class ApiAddCommandHandler : DomainCommandHandler<ApiAddCommand>
{
    private readonly IUnitOfWork _uow;

    public ApiAddCommandHandler(
        IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<bool> Handle(ApiAddCommand request, CancellationToken cancellationToken)
    {
        var apiEntity = new sys_api()
        {
            Id = Guid.NewGuid().ToString("N"),
            createtime = DateTime.Now,
            path = request.Path,
            group = request.Group,
            method = request.Method,
            description = request.Description
        };
        await _uow.GetRepository<sys_api, string>().AddAsync(apiEntity);
        return await _uow.SaveChangesAsync();
    }
}


[Scoped]
public class ApiModifyCommandHandler : DomainCommandHandler<ApiModifyCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IApiManager _apiManager;

    public ApiModifyCommandHandler(
        IUnitOfWork uow, IApiManager apiManager)
    {
        _uow = uow;
        _apiManager = apiManager;
    }

    public override async Task<bool> Handle(ApiModifyCommand request, CancellationToken cancellationToken)
    {
        var apiEntity = await _uow.GetRepository<sys_api, string>().FirstOrDefaultAsync(p => p.Id == request.Id);
        if (null == apiEntity)
            throw new RbacException($"没有找到api实体：{request.Id}", (int)ErrorStatusCode.ApiNotFound);
        apiEntity.method = request.Method;
        apiEntity.path = request.Path;
        apiEntity.group = request.Group;
        apiEntity.description = request.Description;
        _uow.GetRepository<sys_api, string>().Update(apiEntity);
        var result = await _uow.SaveChangesAsync();
        if (result)
            await _apiManager.RemovePermissionCacheAsync(request.Id);
        return result;
    }
}


[Scoped]
public class ApiRemoveCommandHandler : DomainCommandHandler<ApiRemoveCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IApiManager _apiManager;

    public ApiRemoveCommandHandler(
        IUnitOfWork uow, IApiManager apiManager)
    {
        _uow = uow;
        _apiManager = apiManager;
    }

    public override async Task<bool> Handle(ApiRemoveCommand request, CancellationToken cancellationToken)
    {
        var apiEntity = await _uow.GetRepository<sys_api, string>().FirstOrDefaultAsync(p => p.Id == request.Id);
        if (null == apiEntity)
            throw new RbacException($"没有找到api实体：{request.Id}", (int)ErrorStatusCode.ApiNotFound);
        var roleapi = await _uow.GetRepository<sys_role_api, string>().FirstOrDefaultAsync(p => p.apiid == request.Id);
        if (null != roleapi)
            _uow.GetRepository<sys_role_api, string>().Remove(roleapi);
        _uow.GetRepository<sys_api, string>().Remove(apiEntity);
        var result = await _uow.SaveChangesAsync();
        if (result)
            await _apiManager.RemovePermissionCacheAsync(request.Id);
        return result;
    }
}