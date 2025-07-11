using Grpc.Core;

using Ping;

namespace Infrastructure
{
    public static class PingDefinition
    {
        public static readonly Method<PingRequest, PingReply> SayPingMethod =
            new Method<PingRequest, PingReply>(
                MethodType.Unary,
                "Ping.Ping",
                "SayPing",
                ManualMarshallers.PingRequestMarshaller,
                ManualMarshallers.PingReplyMarshaller);
    }
}