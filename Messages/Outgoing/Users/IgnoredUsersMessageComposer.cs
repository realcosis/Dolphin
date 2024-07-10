using Dolphin.HabboHotel.Users.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Users
{
    public class IgnoredUsersMessageComposer(List<IgnoredUser> ignoreds) : OutgoingHandler(ServerPacketCode.IgnoredUsersMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(ignoreds.Count);
            foreach (var ignored in ignoreds)
            {
                Packet?.WriteString(ignored.IgnoredUsername!);
            }
        }
    }
}