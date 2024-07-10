using Dolphin.HabboHotel.Users.Models;
using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Friends
{
    public class FriendRequestsMessageComposer(List<MessengerRequest> requests) : OutgoingHandler(ServerPacketCode.FriendRequestsMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(requests.Count);
            Packet?.WriteInteger(requests.Count);

            foreach (var request in requests) 
            {
                Packet?.WriteInteger(request.FromUser);
                Packet?.WriteString(request.Username!);
                Packet?.WriteString(request.Look!);
            }
        }
    }
}