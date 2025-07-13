using Grpc.Core;

using Arrow;

namespace Infrastructure
{
    public static class ArrowDefinition
    {
        public static readonly Method<ArrowRequest, ArrowReply> SayArrowMethod =
            new Method<ArrowRequest, ArrowReply>(
                MethodType.Unary,
                "Arrow.Arrow",
                "SayArrow",
                ManualMarshallers.ArrowRequestMarshaller,
                ManualMarshallers.ArrowReplyMarshaller);
    }
}