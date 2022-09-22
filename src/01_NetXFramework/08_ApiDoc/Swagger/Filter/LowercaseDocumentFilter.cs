using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetX.Swagger;

/// <summary>
/// 
/// </summary>
public class LowercaseDocumentFilter : IDocumentFilter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = new OpenApiPaths();
        foreach (var path in swaggerDoc.Paths)
        {
            paths.Add(LowercaseEverythingButParameters(path.Key), path.Value);
        }

        swaggerDoc.Paths = paths;
    }

    private static string LowercaseEverythingButParameters(string key)
    {
        //过滤掉路径参数
        return string.Join("/", key.Split('/').Select(x => x.Contains("{") ? x : x.ToLower()));
    }
}
