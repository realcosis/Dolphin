using Dolphin.HabboHotel.Users;
using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing.Users;
using Dolphin.Networking.Client;
using Dolphin.Injection;

namespace Dolphin.Messages.Incoming.Users
{
    [Singleton]
    class UserCurrentBadgesMessageEvent(IUsersManager usersManager) : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
        {
            var userId = packet.ReadInt();

            if (!usersManager.Users.TryGetValue(userId, out var user))
                user = await usersManager.GetHabbo(userId);

            if (user != default)
            {
                var userBadges = user.Badges.Where(b => b.Slot > 0).ToList();
                await client.Send(new UserCurrentBadgesMessageComposer(userBadges, userId));
            }
        }
    }
}