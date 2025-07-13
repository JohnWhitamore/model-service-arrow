using Microsoft.Extensions.Logging;
using Apache.Arrow;

using Arrow;

namespace Services
{
    public class ArrowProcessor
    {
        private readonly ILogger<ArrowProcessor> _logger;

        public ArrowProcessor(ILogger<ArrowProcessor> logger)
        {
            _logger = logger;
        }

        public ArrowReply ProcessBatch(RecordBatch batch)
        {
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

            return new ArrowReply
            {
                ArrayNames = arrayNames,
                ArraySums = arraySums
            };
        }
    }
}

