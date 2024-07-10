using System.Collections.Concurrent;

namespace Dolphin.HabboHotel.Events
{
    public interface IEventsManager
    {
        ConcurrentDictionary<string, Func<object, Task>> Events { get; }

        Task RegisterListener(string eventType, Func<object, Task> listener);

        Task TriggerEvent(string eventType, object eventData);
    }
}