using Grpc.Core;
using Microsoft.Extensions.Logging;

using Ping;

namespace Services
{
    public class PingService
    {
        private readonly ILogger<PingService> _logger;

        public PingService(ILogger<PingService> logger)
        {
            _logger = logger;
            _logger.LogInformation("PingService instantiated.");
        }

        public Task<PingReply> SayPing(PingRequest request, ServerCallContext context)
        {
            _logger.LogInformation("SayPing called with message: {Message}", request.Message);

            var reply = new PingReply
            {
                Message = $"Received ping: {request.Message}"
            };

            _logger.LogInformation("Returning response: {Response}", reply.Message);
            return Task.FromResult(reply);
        }
    }
}