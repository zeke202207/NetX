using Netx.Ddd.Domain;
using NetX.RBAC.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain;

public interface ITestRepository : IRepository<TestEntity, string>
{

}

public interface ITestRepository1 : IRepository<TestEntity1, string>
{

}
