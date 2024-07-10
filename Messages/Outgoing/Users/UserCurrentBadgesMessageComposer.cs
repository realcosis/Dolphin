using Dolphin.HabboHotel.Users.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Users
{
    public class UserCurrentBadgesMessageComposer(List<UserBadge> userBadges, int userId) : OutgoingHandler(ServerPacketCode.UserCurrentBadgesMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(userId);
            Packet?.WriteInteger(userBadges.Count);
            foreach (var userBadge in userBadges)
            {
                Packet?.WriteInteger(userBadge.Slot);
                Packet?.WriteString(userBadge.Code!);
            }
        }
    }
}