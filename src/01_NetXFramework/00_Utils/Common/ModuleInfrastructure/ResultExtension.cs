using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Common.ModuleInfrastructure;

public static class ResultExtension
{
    public static ResultModel ToSuccessResultModel<T>(this T result)
        where T : class
    {
        return new ResultModel<T>(ResultEnum.SUCCESS)
        {
            Result = result
        };
    }

    public static ResultModel ToSuccessResultModel(this bool result)
    {
        return new ResultModel<bool>(ResultEnum.SUCCESS)
        {
            Result = result
        };
    }

    public static ResultModel ToSuccessPagerResultModel<T>(this T result , int total)
    {
        return new PagerResultModel<T>(ResultEnum.SUCCESS)
        {
            Result = result,
            Total = total
        };
    }

    public static ResultModel ToErrorResultModel<T>(this string errorMessage)
        where T : class
    {
        return new ResultModel<T>(ResultEnum.ERROR)
        {
            Message = errorMessage
        };
    }
}
