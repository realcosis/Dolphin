using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Navigator
{
    public class NavigatorSettingsMessageComposer(int homeRoom) : OutgoingHandler(ServerPacketCode.UserObjectMessageComposer)
    {
        public override void Compose()
        {
            Packet?.WriteInteger(homeRoom);
            Packet?.WriteInteger(homeRoom);
        }
    }
}