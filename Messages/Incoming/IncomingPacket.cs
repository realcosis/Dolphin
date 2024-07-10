using DotNetty.Buffers;
using System.Text;

namespace Dolphin.Messages.Incoming
{
    public class IncomingPacket(short id, IByteBuffer buf)
    {
        public string ReadString()
        {
            var length = buf.ReadShort();
            var data = buf.ReadBytes(length);
            return Encoding.UTF8.GetString(data.Array);
        }

        public int ReadInt()
            => buf.ReadInt();

        public bool ReadBoolean()
            => buf.ReadByte() == 1;

        public short Opcode => id;
    }
}