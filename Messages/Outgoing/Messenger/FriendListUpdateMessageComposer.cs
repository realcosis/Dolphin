using Dolphin.HabboHotel.Users.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Messenger
{
    public class FriendListUpdateMessageComposer(MessengerBuddy? buddy, int? friendId, int action, List<MessengerBuddy> buddies) : OutgoingHandler(ServerPacketCode.FriendListUpdateMessageComposer)
    {
        public override void Compose()
        {
            if (buddies.Count == 0)
            {
                Packet?.WriteInteger(0);
                Packet?.WriteInteger(1);
                Packet?.WriteInteger(action);
                if (buddy != default)
                {
                    Packet?.WriteInteger(buddy.UserId);
                    Packet?.WriteString(buddy.Username!);
                    Packet?.WriteInteger(1);
                    Packet?.WriteBoolean(buddy.IsOnline);
                    Packet?.WriteBoolean(buddy.IsOnline);//&& friend.InRoom);
                    Packet?.WriteString(buddy.Look!);
                    Packet?.WriteInteger(0);
                    Packet?.WriteString(buddy.Motto!);
                    Packet?.WriteString(string.Empty);
                    Packet?.WriteString(string.Empty);
                    Packet?.WriteBoolean(true);
                    Packet?.WriteBoolean(false);
                    Packet?.WriteBoolean(false);
                    Packet?.WriteShort((short)buddy.Relationship!);
                }
                else if (friendId.HasValue)
                    Packet?.WriteInteger(friendId.Value);
            }
            else
            {
                Packet?.WriteInteger(0);
                Packet?.WriteInteger(buddies.Count);
                foreach (var buddy in buddies)
                {
                    Packet?.WriteInteger(action);
                    Packet?.WriteInteger(buddy.UserId);
                    Packet?.WriteString(buddy.Username!);
                    Packet?.WriteInteger(1);
                    Packet?.WriteBoolean(buddy.IsOnline);
                    Packet?.WriteBoolean(buddy.IsOnline);//&& friend.InRoom);
                    Packet?.WriteString(buddy.Look!);
                    Packet?.WriteInteger(0);
                    Packet?.WriteString(buddy.Motto!);
                    Packet?.WriteString(string.Empty);
                    Packet?.WriteString(string.Empty);
                    Packet?.WriteBoolean(true);
                    Packet?.WriteBoolean(false);
                    Packet?.WriteBoolean(false);
                    Packet?.WriteShort((short)buddy.Relationship!);
                }
            }
        }
    }
}