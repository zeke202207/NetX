using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Netx.Ddd.Domain;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.RBAC.Models;
using System.Linq;

namespace NetX.RBAC.Domain;

[Scoped]
public class AccountAddCommandHandler : DomainCommandHandler<AccountAddCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IPasswordStrategy _pwdStrategy;

    public AccountAddCommandHandler(
        IUnitOfWork uow,
        IMapper mapper,
        IPasswordStrategy strategy)
    {
        _uow = uow;
        _pwdStrategy = strategy;
    }

    public override async Task<bool> Handle(AccountAddCommand request, CancellationToken cancellationToken)
    {
        var userEntity = new sys_user()
        {
            Id = Guid.NewGuid().ToString("N"),
            nickname = request.NickName,
            username = request.UserName,
            password = await _pwdStrategy.GeneratePassword(),
            avatar = "",
            status = 1,
            remark = request.Remark,
            email = request.Email
        };

        var userroelEntity = new sys_user_role()
        {
            userid = userEntity.Id,
            roleid = request.RoleId
        };

        var userdeptEntity = new sys_user_dept()
        {
            userid = userEntity.Id,
            deptid = request.DeptId
        };

        var user = await _uow.GetRepository<sys_user, string>().AsQueryable()
            .Where(p => p.username.ToLower().Equals(userEntity.username.ToLower()))
            .FirstOrDefaultAsync();
        if (null != user)
            throw new RbacException("用户已经存在", (int)ErrorStatusCode.UserExist);
        await _uow.GetRepository<sys_user, string>().AddAsync(userEntity);
        await _uow.GetRepository<sys_user_role, string>().AddAsync(userroelEntity);
        await _uow.GetRepository<sys_user_dept, string>().AddAsync(userdeptEntity);
        return await _uow.CommitAsync();
    }
}

[Scoped]
public class AccountEditCommandHandler : DomainCommandHandler<AccountEditCommand>
{
    private readonly IUnitOfWork _uow;

    public AccountEditCommandHandler(
        IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<bool> Handle(AccountEditCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.GetRepository<sys_user, string>().AsQueryable()
           .Where(p => p.Id.Equals(request.Id))
           .FirstOrDefaultAsync();
        if (null == user)
            throw new RbacException($"未找到用户：{request.Id}", (int)ErrorStatusCode.UserNotFound);
        user.nickname = request.NickName;
        user.email = request.Email;
        user.remark= request.Remark;
        _uow.GetRepository<sys_user, string>().Update(user);
        if(!string.IsNullOrEmpty(request.DeptId))
            _uow.GetRepository<sys_user_dept, string>().Update(new sys_user_dept() { userid = user.Id, deptid = request.DeptId });
        if (!string.IsNullOrWhiteSpace(request.RoleId))
            _uow.GetRepository<sys_user_role, string>().Update(new sys_user_role() { userid = user.Id, roleid = request.RoleId });
        return await _uow.CommitAsync();
    }
}

[Scoped]
public class AccountRemoveCommandHandler : DomainCommandHandler<AccountRemoveCommand>
{
    private readonly IUnitOfWork _uow;

    public AccountRemoveCommandHandler(
        IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<bool> Handle(AccountRemoveCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.GetRepository<sys_user, string>().FirstOrDefaultAsync(p => p.Id == request.Id);
        if(null ==user)
            throw new RbacException($"未找到用户：{request.Id}", (int)ErrorStatusCode.UserNotFound);
        var roles = _uow.GetRepository<sys_user_role, string>().AsQueryable().Where(p => p.userid == request.Id);
        if (null != roles && roles.Any())
            _uow.GetRepository<sys_user_role, string>().RemoveRange(roles);
        var depts = _uow.GetRepository<sys_user_dept, string>().AsQueryable().Where(p => p.userid == request.Id);
        if (null != depts && depts.Any())
            _uow.GetRepository<sys_user_dept, string>().RemoveRange(depts);
        _uow.GetRepository<sys_user, string>().Remove(user);
        return await _uow.CommitAsync();
    }
}

[Scoped]
public class AccountModifyPwdCommandHandler : DomainCommandHandler<AccountModifyPwdCommand>
{
    private readonly IUnitOfWork _uow;

    public AccountModifyPwdCommandHandler(
        IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<bool> Handle(AccountModifyPwdCommand request, CancellationToken cancellationToken)
    {
        if(string.IsNullOrWhiteSpace(request.OldPwd) || string.IsNullOrWhiteSpace(request.NewPwd))
            throw new RbacException($"密码不能为空：{request.Id}", (int)ErrorStatusCode.PasswordIsNull);
        var user = await _uow.GetRepository<sys_user, string>().AsQueryable()
          .Where(p => p.Id.Equals(request.Id))
          .FirstOrDefaultAsync();
        if (null == user)
            throw new RbacException($"未找到用户：{request.Id}", (int)ErrorStatusCode.UserNotFound);
        if(!request.OldPwd.Equals(user.password))
            throw new RbacException($"密码验证失败：{request.Id}", (int)ErrorStatusCode.PasswordInvalid);
        user.password = request.NewPwd;
        _uow.GetRepository<sys_user, string>().Update(user);
        return await _uow.CommitAsync();
    }
}