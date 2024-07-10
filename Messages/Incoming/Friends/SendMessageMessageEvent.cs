using Dolphin.Backgrounds;
using Dolphin.Backgrounds.Tasks;
using Dolphin.HabboHotel.Users;
using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing.Friends;
using Dolphin.Networking.Client;
using Dolphin.Injection;

namespace Dolphin.Messages.Incoming.Friends
{
    [Singleton]
    class SendMessageMessageEvent(IUsersManager usersManager, IBackgroundManager backgroundManager, IEnumerable<ITask> tasks) : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
        {
            if (client.Habbo == default)
                return;

            var userId = packet.ReadInt();
            var message = packet.ReadString();

            if (!client.Habbo!.Friends.Any(f => f.UserId == userId))
                return;

            if (!usersManager.Users.TryGetValue(userId, out var user))
                return;

            if (user?.ClientSession != default)
            {
                await user.ClientSession.Send(new NewConsoleMessageMessageComposer(client.Habbo.User!.Id, message));
                var task = tasks.OfType<AddPrivateChatlogTask>().FirstOrDefault();
                if (task != default)
                {
                    task.Parameters =
                    [
                        client.Habbo.User.Id,
                        userId,
                        message
                    ];
                    backgroundManager.Queue(task);
                }
            }
        }
    }
}