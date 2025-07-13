using System.Linq;
using Apache.Arrow;
using Apache.Arrow.Ipc;
using Grpc.Core;
using Microsoft.Extensions.Logging;

using Arrow;

namespace Services
{
    public class ArrowService
    {
        private readonly ILogger<ArrowService> _logger;

        public ArrowService(ILogger<ArrowService> logger)
        {
            _logger = logger;
            _logger.LogInformation("ArrowService instantiated.");
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

            var arrayNames = new List<string>();
            var arraySums = new List<double>();

            var fields = batch.Schema.FieldsList;
            var arrays = batch.Arrays.ToList();

            for (int i = 0; i < fields.Count; i++)
            {
                var name = fields[i].Name;
                var array = arrays[i];

                arrayNames.Add(name);
                _logger.LogInformation("Processing array: {Name}", name);

                double sum = 0.0;

                if (array is DoubleArray doubleArray)
                {
                    sum = doubleArray.Values.ToArray().Sum();
                }

                arraySums.Add(sum);
                _logger.LogInformation("Array {Name} sum: {Sum}", name, sum);
            }

            var reply = new ArrowReply
            {
                ArrayNames = arrayNames,
                ArraySums = arraySums
            };

            _logger.LogInformation("Returning reply with {Count} arrays", arrayNames.Count);
            return reply;
        }
    }
}