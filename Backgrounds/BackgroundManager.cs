using Dolphin.Backgrounds.Tasks;
using Dolphin.Injection;
using System.Collections.Concurrent;

namespace Dolphin.Backgrounds
{
    [Singleton]
    class BackgroundManager : IBackgroundManager
    {
        readonly ConcurrentQueue<ITask> workItems = new();

        void IBackgroundManager.Queue(ITask task)
        {
            if (task == default)
                return;

            workItems.Enqueue(task);
        }

        ITask? IBackgroundManager.Dequeue()
            => workItems.TryDequeue(out var task) ? task : default;
    }
}