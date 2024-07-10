using Dolphin.Backgrounds.Tasks;

namespace Dolphin.Backgrounds
{
    public interface IBackgroundManager
    {
        void Queue(ITask task);

        ITask? Dequeue();
    }
}