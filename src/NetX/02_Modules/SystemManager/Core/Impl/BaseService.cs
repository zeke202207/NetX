using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Core.Impl
{
    public abstract class BaseService
    {
        protected string CreateId()
        {
            return Guid.NewGuid().ToString("N");
        }

        protected DateTime CreateInsertTime()
        {
            return DateTime.Now;
        }
    }
}
