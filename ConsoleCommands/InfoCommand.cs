using Dolphin.HabboHotel.Users;
using Microsoft.Extensions.Logging;

namespace Dolphin.ConsoleCommands
{
    class InfoCommand(IUsersManager usersManager, ILogger<InfoCommand> logger) : ConsoleCommand
    {
        protected override async Task Handle()
        {
            await Task.Run(() =>
            {
                logger.LogInformation("{version}", Emulator.Version);
                logger.LogInformation("Developed by Violet02 & Dario9494");
                logger.LogInformation("Thanks to F0ca");
                logger.LogInformation("Utenti Online: {count}", usersManager.Users.Count);
            });
        }
    }
}