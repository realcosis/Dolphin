using Dolphin.Messages.Handler;

namespace Dolphin.Messages.Outgoing.Handshake
{
    public class AuthenticationOkMessageComposer() : OutgoingHandler(ServerPacketCode.AuthenticationOkMessageComposer)
    {
        public override void Compose()
        {

        }
    }
}