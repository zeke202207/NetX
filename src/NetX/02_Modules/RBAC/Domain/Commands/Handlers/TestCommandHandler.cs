using Netx.Ddd.Domain;
using NetX.Common.Attributes;
using NetX.RBAC.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain;

[Scoped]
public class TestCommandHandler : DomainCommandHandler<TestCommand>
{
    private readonly ITestRepository _repository;
    private readonly ITestRepository1 _repository1;

    public TestCommandHandler(ITestRepository repository, ITestRepository1 repository1)
    {
        _repository = repository;
        _repository1 = repository1;
    }

    public override async Task<bool> Handle(TestCommand request, CancellationToken cancellationToken)
    {
        var entity = new TestEntity() { Id =Guid.NewGuid().ToString("N"), Name = request.Name };
        var entity1 = new TestEntity1() { Id = Guid.NewGuid().ToString("N"), Name1 = request.Name };

        var aggregateid = Guid.NewGuid();
        //1.
        entity.AddDomainEvent(new TestEvent(aggregateid) { Entity = entity });
        entity1.AddDomainEvent(new TestEvent1(aggregateid) { Entity = entity1 });

        //2.
        entity.AddDomainEvent(new TestEvent(aggregateid) { Entity = entity });
        entity.AddDomainEvent(new TestEvent1(aggregateid) { Entity = entity1 });

        try
        {
            _repository.Db.Add(entity);
            _repository1.Db.Add(entity1);
            //_repository.Db.Set<TestEntity>().Add(entity);
            //_repository.Db.Set<TestEntity1>().Add()
        }
        catch (Exception ex)
        {

            throw;
        }
        return await _repository.UnitOfWork.CommitAsync();
    }
}
