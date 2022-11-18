using Microsoft.Extensions.Configuration;
using NetX.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.App;

[Singleton]
internal class DefaultStartLogoHandler : IAppStartHandler
{
    private readonly IConfiguration _config;

    public DefaultStartLogoHandler(IConfiguration config)
    {
        this._config = config;
    }

    /// <summary>
    /// 在线生成logo图标
    /// http://patorjk.com/software/taag/#p=display&f=Blocks&t=netx%20
    /// </summary>
    /// <returns></returns>
    public async Task Handle()
    {
        Console.WriteLine("");
        Console.WriteLine
        (@$" 
.-----------------. .----------------.  .----------------.  .----------------.   
| .--------------. || .--------------. || .--------------. || .--------------. |  
| | ____  _____  | || |  _________   | || |  _________   | || |  ____  ____  | |    
| ||_   \|_   _| | || | |_   ___  |  | || | |  _   _  |  | || | |_  _||_  _| | |    
| |  |   \ | |   | || |   | |_  \_|  | || | |_/ | | \_|  | || |   \ \  / /   | |    版本：{this._config.GetValue<string>("netxinfo:version")}
| |  | |\ \| |   | || |   |  _|  _   | || |     | |      | || |    > `' <    | |    
| | _| |_\   |_  | || |  _| |___/ |  | || |    _| |_     | || |  _/ /'`\ \_  | |    {this._config.GetValue<string>("netxinfo:github")}
| ||_____|\____| | || | |_________|  | || |   |_____|    | || | |____||____| | |    
| |              | || |              | || |              | || |              | |    
| '--------------' || '--------------' || '--------------' || '--------------' |  
'----------------'  '----------------'  '----------------'  '----------------'   
        ");
        await Task.CompletedTask;
    }
}
