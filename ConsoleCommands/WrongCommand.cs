using Microsoft.Extensions.Logging;

namespace Dolphin.ConsoleCommands
{
    class WrongCommand(ILogger<WrongCommand> logger) : ConsoleCommand
    {
        protected override async Task Handle()
            => await Task.Run(() =>
            {
                logger.LogError("Comando non supportato!");
            });
    }
}