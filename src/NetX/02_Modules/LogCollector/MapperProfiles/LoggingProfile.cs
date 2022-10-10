using AutoMapper;
using NetX.LogCollector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.LogCollector.MapperProfiles;

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
