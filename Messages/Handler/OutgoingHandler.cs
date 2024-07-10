using Dolphin.Messages.Outgoing;
using DotNetty.Buffers;

namespace Dolphin.Messages.Handler
{
    public abstract class OutgoingHandler(short id)
    {
        public short Id { get; set; } = id;

        protected OutgoingPacket? Packet { get; set; }

        public OutgoingPacket WriteMessage(IByteBuffer buf)
        {
            Packet = new(Id, buf);
            Compose();
            return Packet;
        }

        public abstract void Compose();
    }
}