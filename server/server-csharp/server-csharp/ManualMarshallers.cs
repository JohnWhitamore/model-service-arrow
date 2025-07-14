using Grpc.Core;
using ProtoBuf;

using Ping;
using Arrow;

namespace Infrastructure
{
    public static class ManualMarshallers
    {
        // Ping messages

        // ... request
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

        // ... reply
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

        // Arrow

        // ... request (function call)
        public static readonly Marshaller<ArrowRequest> ArrowRequestMarshaller = new(
            obj =>
            {
                using var ms = new MemoryStream();
                Serializer.Serialize(ms, obj);
                return ms.ToArray();
            },
            data =>
            {
                using var ms = new MemoryStream(data);
                return Serializer.Deserialize<ArrowRequest>(ms);
            });

        // ... reply (function return)
        public static readonly Marshaller<ArrowReply> ArrowReplyMarshaller = new(
            obj =>
            {
                using var ms = new MemoryStream();
                Serializer.Serialize(ms, obj);
                return ms.ToArray();
            },
            data =>
            {
                using var ms = new MemoryStream(data);
                return Serializer.Deserialize<ArrowReply>(ms);
            });



    }
}