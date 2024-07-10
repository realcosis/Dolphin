namespace Dolphin.ConsoleCommands
{
    public interface ICommand
    {
        string[] Parameters { get; set; }

        Task Execute();
    }
}