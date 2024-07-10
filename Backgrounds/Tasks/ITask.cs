namespace Dolphin.Backgrounds.Tasks
{
    public interface ITask
    {
        object[] Parameters { get; set; }

        Task Execute();
    }
}