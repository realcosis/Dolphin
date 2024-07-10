using Dolphin.HabboHotel.Users.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Messenger
{
    public class BuddyListMessageComposer(List<MessengerBuddy> friends, int pages, int page) : OutgoingHandler(ServerPacketCode.BuddyListMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(pages);
            Packet?.WriteInteger(page);
            Packet?.WriteInteger(friends.Count);
            foreach (var friend in friends)
            {
                Packet?.WriteInteger(friend.UserId);
                Packet?.WriteString(friend.Username!);
                Packet?.WriteInteger(1);
                Packet?.WriteBoolean(friend.IsOnline);
                Packet?.WriteBoolean(friend.IsOnline);//&& friend.InRoom);
                Packet?.WriteString(friend.Look!);
                Packet?.WriteInteger(0);
                Packet?.WriteString(friend.Motto!);
                Packet?.WriteString(string.Empty);
                Packet?.WriteString(string.Empty);
                Packet?.WriteBoolean(true);
                Packet?.WriteBoolean(false);
                Packet?.WriteBoolean(false);
                Packet?.WriteShort((short)friend.Relationship!);
            }
        }
    }
}