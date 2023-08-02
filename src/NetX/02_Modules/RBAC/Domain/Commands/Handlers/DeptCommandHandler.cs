using Microsoft.EntityFrameworkCore;
using NetX.Ddd.Domain;
using NetX.Common.Attributes;
using NetX.RBAC.Models;

namespace NetX.RBAC.Domain;

[Scoped]
public class DeptAddCommandHandler : DomainCommandHandler<DeptAddCommand>
{
    private readonly IUnitOfWork _uow;

    public DeptAddCommandHandler(
        IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<bool> Handle(DeptAddCommand request, CancellationToken cancellationToken)
    {
        var deptEntity = new sys_dept()
        {
            Id = Guid.NewGuid().ToString("N"),
            createtime = DateTime.Now,
            deptname = request.DeptName,
            orderno = request.OrderNo,
            parentid = string.IsNullOrWhiteSpace(request.ParentId) ? RBACConst.C_ROOT_DEPT_ID : request.ParentId,
            remark = request.Remark,
            status = int.Parse(request.Status)
        };
        await _uow.GetRepository<sys_dept, string>().AddAsync(deptEntity);
        return await _uow.SaveChangesAsync();
    }
}

[Scoped]
public class DeptModifyCommandHandler : DomainCommandHandler<DeptModifyCommand>
{
    private readonly IUnitOfWork _uow;

    public DeptModifyCommandHandler(
        IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<bool> Handle(DeptModifyCommand request, CancellationToken cancellationToken)
    {
        var deptEntity = await _uow.GetRepository<sys_dept, string>().FirstOrDefaultAsync(p => p.Id == request.Id);
        if (null == deptEntity)
            throw new RbacException($"没有找到部门信息：{request.Id}", (int)ErrorStatusCode.DeptNotFound);
        deptEntity.deptname = request.DeptName;
        deptEntity.orderno = request.OrderNo;
        deptEntity.parentid = string.IsNullOrWhiteSpace(request.ParentId) ? RBACConst.C_ROOT_DEPT_ID : request.ParentId;
        deptEntity.remark = request.Remark;
        deptEntity.status = int.Parse(request.Status);
        _uow.GetRepository<sys_dept, string>().Update(deptEntity);
        return await _uow.SaveChangesAsync();
    }
}

[Scoped]
public class DeptRemoveCommandHandler : DomainCommandHandler<DeptRemoveCommand>
{
    private readonly IUnitOfWork _uow;

    public DeptRemoveCommandHandler(
        IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<bool> Handle(DeptRemoveCommand request, CancellationToken cancellationToken)
    {
        var deptEntity = await _uow.GetRepository<sys_dept, string>().FirstOrDefaultAsync(p => p.Id == request.Id);
        if (null == deptEntity)
            throw new RbacException($"没有找到deptEntity实体：{request.Id}", (int)ErrorStatusCode.DeptNotFound);
        var userdept = await _uow.GetRepository<sys_user_dept, string>().FirstOrDefaultAsync(p => p.deptid == request.Id);
        if (null != userdept)
            _uow.GetRepository<sys_user_dept, string>().Remove(userdept);
        _uow.GetRepository<sys_dept, string>().Remove(deptEntity);
        return await _uow.SaveChangesAsync();

    }
}