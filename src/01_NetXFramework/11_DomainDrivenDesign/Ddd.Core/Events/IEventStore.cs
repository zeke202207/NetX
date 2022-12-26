namespace Netx.Ddd.Core;

/// <summary>
/// Defines a IEvent Store interface 
/// 
/// </summary>
public interface IEventStore
{
    void Save<T>(T @event) where T : EventBase;
}
