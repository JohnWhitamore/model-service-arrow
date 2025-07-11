using Grpc.Core;
using ProtoBuf;

using Ping;

namespace Infrastructure
{
    public static class ManualMarshallers
    {
        public static readonly Marshaller<PingRequest> PingRequestMarshaller = new(
            obj =>
            {
                using var ms = new MemoryStream();
                Serializer.Serialize(ms, obj);
                return ms.ToArray();
            },
            data =>
            {
                using var ms = new MemoryStream(data);
                return Serializer.Deserialize<PingRequest>(ms);
            });

        public static readonly Marshaller<PingReply> PingReplyMarshaller = new(
            obj =>
            {
                using var ms = new MemoryStream();
                Serializer.Serialize(ms, obj);
                return ms.ToArray();
            },
            data =>
            {
                using var ms = new MemoryStream(data);
                return Serializer.Deserialize<PingReply>(ms);
            });
    }
}