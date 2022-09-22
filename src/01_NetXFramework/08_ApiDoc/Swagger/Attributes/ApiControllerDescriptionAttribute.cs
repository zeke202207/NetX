namespace NetX.Swagger;

/// <summary>
/// Controller描述特性
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ApiControllerDescriptionAttribute : Attribute, Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionGroupNameProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="groupName"></param>
    public ApiControllerDescriptionAttribute(string groupName)
    {
        GroupName = groupName;
    }

    /// <summary>
    /// 组名
    /// </summary>
    public string GroupName { get; set; }

    /// <summary>
    /// http请求Header的key值
    /// </summary>
    public string[]? HeaderKeys { get; set; }

    /// <summary>
    /// 控制器描述
    /// </summary>
    public string? Description { get; set; }
}
