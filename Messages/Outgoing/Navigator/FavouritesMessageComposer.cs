using Dolphin.HabboHotel.Rooms.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Navigator
{
    public class FavouritesMessageComposer(List<Room> favouritesRooms) : OutgoingHandler(ServerPacketCode.UserObjectMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(50);
            Packet?.WriteInteger(favouritesRooms.Count);
            foreach (var favouritesRoom in favouritesRooms)
                Packet?.WriteInteger(favouritesRoom.Id);
        }
    }
}