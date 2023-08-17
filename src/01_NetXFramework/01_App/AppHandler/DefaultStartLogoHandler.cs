using Microsoft.Extensions.Configuration;
using NetX.Common;
using NetX.Common.Attributes;

namespace NetX.App;

[Singleton]
internal class DefaultStartLogoHandler : IStartHandler
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
        await Task.CompletedTask;
    }
}
