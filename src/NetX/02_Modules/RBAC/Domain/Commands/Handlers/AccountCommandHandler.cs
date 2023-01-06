using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Netx.Ddd.Domain;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.RBAC.Models;

namespace NetX.RBAC.Domain;

[Scoped]
public class AccountAddCommandHandler : DomainCommandHandler<AccountAddCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IPasswordStrategy _pwdStrategy;

    public AccountAddCommandHandler(IUnitOfWork uow,
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
