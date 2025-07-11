using ProtoBuf;

namespace Ping
{
    [ProtoContract]
    public class PingRequest
    {
        [ProtoMember(1)]
        public string Message { get; set; } = string.Empty;
    }

    [ProtoContract]
    public class PingReply
    {
        [ProtoMember(1)]
        public string Message { get; set; } = string.Empty;
    }
}