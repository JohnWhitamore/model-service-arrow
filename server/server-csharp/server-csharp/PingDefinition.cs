using Grpc.Core;

using Ping;

namespace Infrastructure
{
    public static class PingDefinition
    {
        public static readonly Method<PingRequest, PingReply> SayPingMethod =
            new Method<PingRequest, PingReply>(
                MethodType.Unary,
                "Ping.Ping",    // Matches the service and package name in ping.proto
                "SayPing",
                ManualMarshallers.PingRequestMarshaller,
                ManualMarshallers.PingReplyMarshaller);
    }
}