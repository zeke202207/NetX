using System.Threading.Channels;

namespace NetX.EventBus;

/// <summary>
/// 基于内存管道的事件源记录器
/// </summary>
internal sealed class ChannelEventSourceStorer : IEventSourceStorer
{
    /// <summary>
    /// 内存管道事件源记录器
    /// </summary>
    private readonly Channel<IEventSource> _channel;

    /// <summary>
    /// 事件源记录器对象
    /// </summary>
    /// <param name="capacity">存储器最大事件数量，超过这个数量，将丢弃最早的数据</param>
    public ChannelEventSourceStorer(int capacity)
    {
        var options = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };
        _channel = Channel.CreateBounded<IEventSource>(options);
    }

    /// <summary>
    /// 事件溯源写入器
    /// </summary>
    /// <param name="eventSource">事件源对象</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async ValueTask WriteAsync(IEventSource eventSource, CancellationToken cancellationToken)
    {
        if (null == eventSource)
            throw new ArgumentNullException(nameof(eventSource));
        await _channel.Writer.WriteAsync(eventSource);
    }

    /// <summary>
    /// 事件源读取器
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async ValueTask<IEventSource> ReadAsync(CancellationToken cancellationToken)
    {
        var eventSource = await _channel.Reader.ReadAsync();
        return eventSource;
    }
}
