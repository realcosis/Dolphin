using Dolphin.HabboHotel.Groups;
using Dolphin.HabboHotel.Users;
using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing.Groups;
using Dolphin.Networking.Client;
using Dolphin.Injection;

namespace Dolphin.Messages.Incoming.Groups
{
    [Scoped]
    class GroupInformationMessageEvent(IGroupsManager groupsManager, IUsersManager usersManager) : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
        {
            if (client.Habbo == default || client.Habbo?.User == default)
                return;

            var groupId = packet.ReadInt();
            var newWindow = packet.ReadBoolean();

            var group = await groupsManager.GetGroup(groupId);
            if (group != default)
            {
                var ownerName = (await usersManager.GetHabbo(group.OwnerId))?.User?.Username;
                await client.Send(new GroupInformationMessageComposer(group, client.Habbo, newWindow, ownerName));
            }                
        }
    }
}