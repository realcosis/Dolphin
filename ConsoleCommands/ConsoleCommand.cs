using Dolphin.Injection;

namespace Dolphin.ConsoleCommands
{
    [Singleton]
    public abstract class ConsoleCommand : ICommand
    {
        string[] ICommand.Parameters { get; set; } = [];

        async Task ICommand.Execute()
            => await Handle();

        protected abstract Task Handle();
    }
}