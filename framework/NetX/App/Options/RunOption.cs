namespace NetX;

/// <summary>
/// <see cref="WebApplication"/> 方式进行配置
/// </summary>
public sealed class RunOption
{
    /// <summary>
    /// <see cref="WebApplicationOptions"/>
    /// </summary>
    internal WebApplicationOptions Options { get; set; }

    /// <summary>
    /// 默认配置项
    /// </summary>
    public static RunOption Default { get; } = new RunOption();

    /// <summary>
    /// 内部配置类实例
    /// </summary>
    internal RunOption()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public RunOption ConfigOption(WebApplicationOptions options)
    {
        Options = options;
        return this;
    }
}

