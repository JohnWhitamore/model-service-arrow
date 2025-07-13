using ProtoBuf;

namespace Arrow
{
    [ProtoContract]
    public class ArrowRequest
    {
        [ProtoMember(1)]
        public byte[] Data { get; set; } = Array.Empty<byte>();
    }

    [ProtoContract]
    public class ArrowReply
    {
        [ProtoMember(1)]
        public List<string> ArrayNames { get; set; } = new();

        [ProtoMember(2)]
        public List<double> ArraySums { get; set; } = new();
    }
}