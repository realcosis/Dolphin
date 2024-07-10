using Dolphin.HabboHotel.Users.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Users
{
    public class WardrobeMessageComposer(List<UserWardrobe> wardrobes) : OutgoingHandler(ServerPacketCode.WardrobeMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(1);
            Packet?.WriteInteger(wardrobes.Count);
            foreach (var wardrobe in wardrobes)
            {
                Packet?.WriteInteger(wardrobe.SlotId);
                Packet?.WriteString(wardrobe.Look!);
                Packet?.WriteString(wardrobe.Gender!);
            }
        }
    }
}