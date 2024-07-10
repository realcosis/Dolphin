using Microsoft.Extensions.DependencyInjection;

namespace Dolphin.ConsoleCommands
{
    public static class ConsoleCommandHandler
    {
        public static async Task InvokeCommand(this string? inputData, IServiceProvider services)
        {
            if (string.IsNullOrWhiteSpace(inputData))
                return;

            var commands = services.GetRequiredService<IEnumerable<ICommand>>();
            var commandParameters = inputData.Split(' ');
            ICommand command = commandParameters[0].ToLower() switch
            {
                "shutdown" => commands.OfType<ShutdownCommand>().FirstOrDefault()!,
                "info" => commands.OfType<InfoCommand>().FirstOrDefault()!,
                "debug" => commands.OfType<DebugCommand>().FirstOrDefault()!,
                _ => commands.OfType<WrongCommand>().FirstOrDefault()!
            };

            command.Parameters = commandParameters.Skip(1).ToArray();
            await command.Execute();
        }
    }
}