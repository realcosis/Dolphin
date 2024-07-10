using Dolphin.HabboHotel.Users.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Users
{
    public class UserObjectMessageComposer(Habbo? habbo) : OutgoingHandler(ServerPacketCode.UserObjectMessageComposer)
    {
        public override void Compose()
        {
            if (habbo == default || habbo.User == default)
                return;

            Packet?.WriteInteger(habbo.User.Id);
            Packet?.WriteString(habbo.User.Username!);
            Packet?.WriteString(habbo.User.Look!);
            Packet?.WriteString(habbo.User.Gender!);
            Packet?.WriteString(habbo.User.Motto!);
            Packet?.WriteString(habbo.User.Username!);
            Packet?.WriteBoolean(true);
            Packet?.WriteInteger(0);
            Packet?.WriteInteger(0);
            Packet?.WriteInteger(0);
            Packet?.WriteBoolean(false);
            Packet?.WriteString("01-01-1970 00:00:00");
            Packet?.WriteBoolean(false);
            Packet?.WriteBoolean(false);
        }
    }
}