using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace NetX.Swagger;

/// <summary>
/// 
/// </summary>
public class AddRequiredHeaderParameter : IOperationFilter
{
    /// <summary>
    /// 
    /// </summary>
    public static string? HeaderKey { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();
        var des = context.ApiDescription;
        if (null != des && des.TryGetMethodInfo(out MethodInfo methodInfo) && null != methodInfo.DeclaringType)
        {
            var desc = Attribute.GetCustomAttribute(methodInfo.DeclaringType, typeof(ApiControllerDescriptionAttribute));
            if (desc is ApiControllerDescriptionAttribute)
            {
                var descAttr = (ApiControllerDescriptionAttribute)desc;
                if (null != descAttr.HeaderKeys)
                {
                    foreach (var item in descAttr.HeaderKeys)
                    {
                        if (operation.Parameters.Any(p => p.Name.ToLower() == item.ToLower()))
                            continue;
                        operation.Parameters.Add(new OpenApiParameter()
                        {
                            Name = item,
                            In = ParameterLocation.Header,
                            Required = false,
                            AllowEmptyValue = true,
                        });
                    }
                }
            }
        }

    }
}
