using Dolphin.HabboHotel.Groups;
using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing.Groups;
using Dolphin.Networking.Client;
using Dolphin.Injection;

namespace Dolphin.Messages.Incoming.Groups
{
    [Singleton]
    class GroupBadgePartsMessageEvent(IGroupsManager groupsManager) : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
        {
            var groupsElements = groupsManager.GroupElements.Values.ToList();
            await client.Send(new GroupBadgePartsMessageComposer(groupsElements));
        }
    }
}