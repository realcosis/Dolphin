using RC.Common.Injection;
using System.Collections.Concurrent;

namespace Dolphin.HabboHotel.Events
{
    [Scoped]
    class EventsManager : IEventsManager
    {
        ConcurrentDictionary<string, Func<object, Task>> IEventsManager.Events { get; } = [];

        async Task IEventsManager.RegisterListener(string eventType, Func<object, Task> listener)
        {
            await Task.Yield();

            ((IEventsManager)this).Events.AddOrUpdate(eventType, listener, (key, existingListener) => existingListener + listener);
        }

        async Task IEventsManager.TriggerEvent(string eventType, object eventData)
        {
            if (((IEventsManager)this).Events.TryGetValue(eventType, out var listener))
                if (listener != default)
                    await listener(eventData);
        }
    }
}