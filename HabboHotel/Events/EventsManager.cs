using Dolphin.Injection;
using System.Collections.Concurrent;

namespace Dolphin.HabboHotel.Events
{
    [Scoped]
    class EventsManager : IEventsManager
    {
        ConcurrentDictionary<string, Func<object, Task>> IEventsManager.Events { get; } = [];

        void IEventsManager.RegisterListener(string eventType, Func<object, Task> listener)
            => ((IEventsManager)this).Events.AddOrUpdate(eventType, listener, (key, existingListener) => existingListener + listener);

        async Task IEventsManager.TriggerEvent(string eventType, object eventData)
        {
            if (((IEventsManager)this).Events.TryGetValue(eventType, out var listener))
                if (listener != default)
                    await listener(eventData);
        }

        void IEventsManager.UnregisterListener(string eventType)
            => ((IEventsManager)this).Events.TryRemove(eventType, out var _);
    }
}