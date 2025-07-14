using Apache.Arrow.Ipc;
using Grpc.Core;
using Microsoft.Extensions.Logging;

using Arrow;

namespace Services
{
    public class ArrowService
    {
        private readonly ILogger<ArrowService> _logger;
        private readonly ArrowProcessor _processor;

        public ArrowService(ILogger<ArrowService> logger, ArrowProcessor processor)
        {
            _logger = logger;
            _logger.LogInformation("ArrowService instantiated.");

            _processor = processor;
        }

        public async Task<ArrowReply> SayArrow(ArrowRequest request, ServerCallContext context)
        {
            _logger.LogInformation("SayArrow called with {ByteCount} bytes", request.Data.Length);

            using var stream = new MemoryStream(request.Data);
            var reader = new ArrowStreamReader(stream);
            var batch = await reader.ReadNextRecordBatchAsync();

            if (batch == null)
            {
                _logger.LogWarning("Arrow batch was null or empty.");
                return new ArrowReply();
            }

            var reply = _processor.ProcessBatch(batch);

            _logger.LogInformation("Returning reply with {Count} arrays", reply.ArrayNames.Count);
            return reply;
        }
    }
}