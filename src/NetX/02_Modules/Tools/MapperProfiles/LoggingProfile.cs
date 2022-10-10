using AutoMapper;
using NetX.Tools.Models;

namespace NetX.Tools.MapperProfiles;

/// <summary>
/// logging 映射清单
/// </summary>
public class LoggingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public LoggingProfile()
    {
        ToEntity();
        ToModel();
    }

    private void ToEntity()
    {
        CreateMap<LoggingModel, sys_logging>();
    }

    private void ToModel()
    {
        CreateMap<sys_logging, LoggingModel>();
    }
}
