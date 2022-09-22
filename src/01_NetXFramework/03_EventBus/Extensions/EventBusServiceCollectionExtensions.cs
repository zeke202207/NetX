using Microsoft.Extensions.DependencyInjection;

namespace NetX.EventBus;

/// <summary>
/// 事件总线服务注入扩展
/// </summary>
public static class EventBusServiceCollectionExtensions
{
    /// <summary>
    /// 添加 EventBus 模块注册
    /// </summary>
    /// <param name="services">服务集合对象</param>
    /// <param name="configureOptionsBuilder">事件总线配置选项构建器委托</param>
    /// <returns>服务集合实例</returns>
    public static IServiceCollection AddEventBus(this IServiceCollection services, Action<EventBusOptionsBuilder> configureOptionsBuilder)
    {
        // 创建初始事件总线配置选项构建器
        var eventBusOptionsBuilder = new EventBusOptionsBuilder();
        configureOptionsBuilder.Invoke(eventBusOptionsBuilder);
        return services.AddEventBus(eventBusOptionsBuilder);
    }

    /// <summary>
    /// 添加 EventBus 模块注册
    /// </summary>
    /// <param name="services">服务集合对象</param>
    /// <param name="eventBusOptionsBuilder">事件总线配置选项构建器</param>
    /// <returns>服务集合实例</returns>
    public static IServiceCollection AddEventBus(this IServiceCollection services, EventBusOptionsBuilder? eventBusOptionsBuilder = default)
    {
        // 初始化事件总线配置项
        eventBusOptionsBuilder ??= new EventBusOptionsBuilder();

        // 注册内部服务
        services.AddInternalService(eventBusOptionsBuilder);

        // 通过工厂模式创建
        services.AddHostedService(serviceProvider =>
        {
            // 创建事件总线后台服务对象
            var eventBusHostedService = ActivatorUtilities.CreateInstance<EventBusHostService>(serviceProvider);

            return eventBusHostedService;
        });

        // 构建事件总线服务
        eventBusOptionsBuilder.Build(services);

        return services;
    }

    /// <summary>
    /// 注册内部服务
    /// </summary>
    /// <param name="services">服务集合对象</param>
    /// <param name="eventBusOptions">事件总线配置选项</param>
    /// <returns>服务集合实例</returns>
    private static IServiceCollection AddInternalService(this IServiceCollection services, EventBusOptionsBuilder eventBusOptions)
    {
        // 注册后台任务队列接口/实例为单例，采用工厂方式创建
        services.AddSingleton<IEventSourceStorer>(_ =>
        {
            // 创建默认内存通道事件源对象
            return new ChannelEventSourceStorer(eventBusOptions.ChannelCapacity);
        });

        // 注册默认内存通道事件发布者
        services.AddSingleton<IEventPublisher, EventPublisher>();

        return services;
    }
}
