using Dolphin.Messages.Handler;
using Dolphin.Messages.Outgoing;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Dolphin.Networking.Encoder
{
    public class GameEncoder : MessageToByteEncoder<OutgoingHandler>
    {
        protected override void Encode(IChannelHandlerContext context, OutgoingHandler message, IByteBuffer output)
        {
            OutgoingPacket packet = message.WriteMessage(output);
            packet.FinalizeBuffer();
        }
    }
}