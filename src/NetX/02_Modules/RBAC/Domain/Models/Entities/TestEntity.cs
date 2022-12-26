using Netx.Ddd.Domain;
using Netx.Ddd.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain.Models.Entities;

public class TestEntity : BaseEntity<string>
{
    public string Name { get; set; }
}


public class TestEntity1 : BaseEntity<string>
{
    public string Name1 { get; set; }
}

