using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Ddd.Domain;

[AttributeUsage(AttributeTargets.Class)]
public class UPKeyAttribute : Attribute
{
    public string[] KeyNames { get; set; }

    public UPKeyAttribute(params string[] keyName)
    {
        KeyNames = keyName;
    }
}
