using Dolphin.HabboHotel.Users;
using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing.Users;
using Dolphin.Networking.Client;
using RC.Common.Injection;

namespace Dolphin.Messages.Incoming.Users
{
    [Singleton]
    class GetExtendedProfileMessageEvent(IUsersManager usersManager) : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
        {
            var userId = packet.ReadInt();

            if (!usersManager.Users.TryGetValue(userId, out var user))
                user = await usersManager.GetHabbo(userId);

            if (user != default)
                await client.Send(new ExtendedProfileMessageComposer(user, client.Habbo!));
        }
    }
}