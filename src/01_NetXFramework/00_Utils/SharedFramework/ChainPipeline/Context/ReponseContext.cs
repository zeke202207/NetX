using System.Runtime;

namespace NetX.SharedFramework.ChainPipeline;

/// <summary>
/// 管道执行结果
/// </summary>
public sealed class ReponseContext<T>
    where T : new()
{
    public T Result { get; }

    public bool Success { get; set; }

    public List<Exception> Ex { get; set; } = new List<Exception>();

    public ReponseContext()
    {
        Result = new T();
    }
}
