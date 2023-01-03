using Netx.Ddd.Domain;
using NetX.Common.Attributes;
using NetX.RBAC.Domain.Models.Entities;

namespace NetX.RBAC.Domain;

[Scoped]
public class TestCommandHandler : DomainCommandHandler<TestCommand>
{
    private readonly IUnitOfWork _uow;

    public TestCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<bool> Handle(TestCommand request, CancellationToken cancellationToken)
    {
        var entity = new TestEntity() { Id = Guid.NewGuid().ToString("N"), Name = request.Name };
        var entity1 = new TestEntity1() { Id = Guid.NewGuid().ToString("N"), Name1 = request.Name };
        try
        {
            _uow.GetRepository<TestEntity, string>().Add(entity);
            _uow.GetRepository<TestEntity1, string>().Add(entity1);

            var r = _uow.GetRepository<TestEntity, string>().AsQueryable().Where(p => p.Id != "1").ToList();
        }
        catch (Exception ex)
        {
            throw;
        }
        return await _uow.CommitAsync();
    }
}
