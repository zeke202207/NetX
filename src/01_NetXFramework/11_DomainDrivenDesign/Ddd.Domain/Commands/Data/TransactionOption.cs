using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Ddd.Domain
{
    public class TransactionOption
    {
        public Type EntityType { get; private set; }

        public Action<object> DataBaseInvoke { get; set; }

        public TransactionOption(Type entityType)
        {
            EntityType = entityType;
        }
    }
}
