using Dolphin.HabboHotel.Rooms;
using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing.Navigator;
using Dolphin.Networking.Client;
using Dolphin.Injection;

namespace Dolphin.Messages.Incoming.Navigator
{
    [Singleton]
    class GetUserFlatCatsMessageEvent(IRoomsManager roomsManager) : IIncomingHandler
    {
        async Task IIncomingHandler.Handle(ClientSession client, IncomingPacket packet)
        {
            if (client.Habbo == default || client.Habbo?.User == default)
                return;

            var navigatorCategories = roomsManager.NavigatorCategories.Select(nc => nc.Value).Where(nc => nc.MinRank <= client.Habbo!.User!.Rank).ToList();
            await client.Send(new UserFlatCatsMessageComposer(navigatorCategories));
        }
    }
}