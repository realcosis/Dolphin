using DotNetty.Buffers;
using DotNetty.Common;
using System.Text;

namespace Dolphin.Messages.Outgoing
{
    public class OutgoingPacket : IByteBufferHolder
    {
        readonly short _id;
        readonly IByteBuffer _buffer;

        public OutgoingPacket(short id, IByteBuffer body)
        {
            _buffer = body;
            _id = id;
            if (body.WriterIndex == 0)
            {
                _buffer.WriteInt(-1);
                _buffer.WriteShort(id);
            }
        }

        public void WriteByte(byte b)
            => _buffer.WriteByte(b);

        public void WriteByte(int b)
            => _buffer.WriteByte((byte)b);

        public void WriteDouble(double d)
            => _buffer.WriteDouble(d);

        public void WriteString(string s)
        {
            var data = Encoding.UTF8.GetBytes(s);
            _buffer.WriteShort(data.Length);
            _buffer.WriteBytes(data);
        }

        public void WriteShort(int s)
            => _buffer.WriteShort(s);

        public void WriteInteger(int i)
            => _buffer.WriteInt(i);

        public void WriteBoolean(bool b)
            => _buffer.WriteByte(b ? 1 : 0);

        public int Length => _buffer.WriterIndex - 4;

        public void FinalizeBuffer()
            => _buffer.SetInt(0, Length);

        public IByteBuffer Content => _buffer;

        public int ReferenceCount => _buffer.ReferenceCount;

        public IByteBufferHolder Copy()
            => new OutgoingPacket(_id, _buffer.Copy());

        public IByteBufferHolder Duplicate()
            => new OutgoingPacket(_id, _buffer.Duplicate());

        public IByteBufferHolder RetainedDuplicate()
            => new OutgoingPacket(_id, _buffer.RetainedDuplicate());

        public IByteBufferHolder Replace(IByteBuffer content)
            => new OutgoingPacket(_id, content);

        public IReferenceCounted Retain()
            => _buffer.Retain();

        public IReferenceCounted Retain(int increment)
            => _buffer.Retain(increment);

        public IReferenceCounted Touch()
            => _buffer.Touch();

        public IReferenceCounted Touch(object hint)
            => _buffer.Touch(hint);

        public bool Release()
            => _buffer.Release();

        public bool Release(int decrement)
            => _buffer.Release(decrement);
    }
}