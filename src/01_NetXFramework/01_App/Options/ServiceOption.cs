using ServiceSelf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.App.Options;

public class ServiceOption
{
    public string ServiceName { get; set; } = "netx";

    public ServiceOptions Options { get; set; }
}
