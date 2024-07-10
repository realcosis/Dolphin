using Dolphin.Messages.Incoming;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Dolphin.Networking.Decoder
{
    public class GameDecoder : MessageToMessageDecoder<IByteBuffer>
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer message, List<object> output)
        {
            try
            {
                short id = message.ReadShort();
                IncomingPacket packet = new(id, message.ReadBytes(message.ReadableBytes));

                output.Add(packet);
            }
            catch (Exception)
            {
            }
        }
    }
}