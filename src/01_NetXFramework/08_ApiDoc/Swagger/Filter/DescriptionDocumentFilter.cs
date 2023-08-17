using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace NetX.Swagger;

/// <summary>
/// 控制器和方法的描述信息处理
/// </summary>
public class DescriptionDocumentFilter : IDocumentFilter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        SetControllerDescription(swaggerDoc, context);
        SetActionDescription(swaggerDoc, context);
        SetModelDescription(swaggerDoc, context);
    }

    /// <summary>
    /// 设置控制器的描述信息
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    private void SetControllerDescription(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (swaggerDoc.Tags == null)
            swaggerDoc.Tags = new List<OpenApiTag>();
        foreach (var apiDescription in context.ApiDescriptions)
        {
            if (apiDescription.TryGetMethodInfo(out MethodInfo methodInfo) && methodInfo.DeclaringType != null)
            {
                var desc = Attribute.GetCustomAttribute(methodInfo.DeclaringType, typeof(ApiControllerDescriptionAttribute));
                if (desc is ApiControllerDescriptionAttribute)
                {
                    var descAttr = (ApiControllerDescriptionAttribute)desc;
                    if (string.IsNullOrWhiteSpace(descAttr.Description))
                        continue;
                    var controllerName = methodInfo.DeclaringType.Name;
                    controllerName = controllerName.Remove(controllerName.Length - 10);
                    if (swaggerDoc.Tags.All(t => t.Name != controllerName))
                    {
                        swaggerDoc.Tags.Add(new OpenApiTag
                        {
                            Name = controllerName,
                            Description = descAttr.Description
                        });
                    }
                }
            }
        }
    }

    /// <summary>
    /// 设置方法的说明
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    private void SetActionDescription(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var path in swaggerDoc.Paths)
        {
            if (TryGetActionDescription(path.Key, context, out string description))
            {
                if (path.Value != null && path.Value.Operations != null && path.Value.Operations.Any())
                {
                    var operation = path.Value.Operations.FirstOrDefault();
                    operation.Value.Summary = description;
                    operation.Value.Description = $"{path.Key}";
                }
            }
        }
    }

    /// <summary>
    /// 设置模型属性描述信息
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    private void SetModelDescription(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var pro = typeof(SchemaRepository).GetField("_reservedIds", BindingFlags.NonPublic | BindingFlags.Instance);
        if (pro == null)
            return;
        var schemaTypes = (Dictionary<Type, string>?)pro.GetValue(context.SchemaRepository);
        if (null == schemaTypes)
            return;
        foreach (var schema in context.SchemaRepository.Schemas)
        {
            var type = schemaTypes.FirstOrDefault(m => m.Value.Equals(schema.Key, StringComparison.OrdinalIgnoreCase)).Key;
            if (type == null || !type.IsClass)
                continue;
            var properties = type.GetProperties();
            foreach (var propertyInfo in properties)
            {
                var propertySchema = schema.Value.Properties.FirstOrDefault(m => m.Key.Equals(propertyInfo.Name, StringComparison.OrdinalIgnoreCase)).Value;
                if (propertySchema != null)
                {
                    var desc = Attribute.GetCustomAttribute(propertyInfo, typeof(ApiControllerDescriptionAttribute));
                    if (desc is ApiControllerDescriptionAttribute)
                    {
                        var descAttr = (ApiControllerDescriptionAttribute)desc;
                        if (!string.IsNullOrWhiteSpace(descAttr.Description))
                            propertySchema.Title = descAttr.Description;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 获取说明
    /// </summary>
    private bool TryGetActionDescription(string path, DocumentFilterContext context, out string description)
    {
        foreach (var apiDescription in context.ApiDescriptions)
        {
            var apiPath = "/" + apiDescription.RelativePath?.ToLower();
            if (apiPath.Equals(path) && apiDescription.TryGetMethodInfo(out MethodInfo methodInfo))
            {
                var desc = Attribute.GetCustomAttribute(methodInfo, typeof(ApiActionDescriptionAttribute));
                if (desc is ApiActionDescriptionAttribute)
                {
                    var descAttr = (ApiActionDescriptionAttribute)desc;
                    if (!string.IsNullOrWhiteSpace(descAttr.Description))
                    {
                        description = descAttr.Description;
                        return true;
                    }
                }
            }
        }
        description = "";
        return false;
    }
}
