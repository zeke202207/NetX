using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Swagger;

public class AddRequiredHeaderParameter : IOperationFilter
{
    public static string HeaderKey { get; set; }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();
        var des = context.ApiDescription;
        if(null != des && des.TryGetMethodInfo(out MethodInfo methodInfo))
        {
            var descAttr = (ApiControllerDescriptionAttribute)Attribute.GetCustomAttribute(methodInfo.DeclaringType, typeof(ApiControllerDescriptionAttribute));
            if(null != descAttr && descAttr.HeaderKeys?.Length>0)
            {
                foreach(var item in descAttr.HeaderKeys)
                {
                    if (operation.Parameters.Any(p => p.Name.ToLower() == item.ToLower()))
                        continue;
                    operation.Parameters.Add(new OpenApiParameter()
                    {
                        Name = item,
                        In = ParameterLocation.Header,
                        Required = false,
                        AllowEmptyValue=true,
                    });
                }
            }
        }
      
    }
}
