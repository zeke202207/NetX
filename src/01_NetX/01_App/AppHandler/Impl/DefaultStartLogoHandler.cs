using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.App;

internal class DefaultStartLogoHandler : IAppStartHandler
{
    public async Task Handle()
    {
        Console.WriteLine();
        Console.WriteLine(@" ********************************************************************************************");
        Console.WriteLine(@" *****                                 Hello,NetX                                      ******");
        Console.WriteLine(@" ********************************************************************************************");
        await Task.CompletedTask;
    }
}
