using Microsoft.EntityFrameworkCore;
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
public class TestRepository : ITestRepository
{
    protected readonly NetxContext _context;

    public TestRepository(NetxContext context)
    {
        _context = context;
        _context.Set<TestEntity>();
    }

    public IUnitOfWork UnitOfWork => _context;

    public DbContext Db => _context;

    public void Dispose()
    {
        Db?.Dispose();
    }
}


[Scoped]
public class TestRepository1 : ITestRepository1
{
    protected readonly NetxContext _context;

    public TestRepository1(NetxContext context)
    {
        _context = context;
        _context.Set<TestEntity1>();
    }

    public IUnitOfWork UnitOfWork => _context;

    public DbContext Db => _context;

    public void Dispose()
    {
        Db?.Dispose();
    }
}
