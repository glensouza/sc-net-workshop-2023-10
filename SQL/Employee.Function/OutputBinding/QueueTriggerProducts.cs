using System;
using Employee.Function.Common;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Employee.Function.OutputBinding
{
    public class QueueTriggerProducts
    {
        [FunctionName("QueueTriggerProducts")]
        public static void Run(
            [QueueTrigger("testqueue")] string queueMessage, ILogger log,
            [Sql("[dbo].[Products]", "SqlConnectionString")] ICollector<Product> products)
        {
            int totalUpserts = 100;
            log.LogInformation($"[QueueTrigger]: {DateTime.Now} starting execution {queueMessage}. Rows to generate={totalUpserts}.");

            var sw = new Stopwatch();
            sw.Start();

            List<Product> newProducts = ProductUtilities.GetNewProducts(totalUpserts);
            foreach (Product product in newProducts)
            {
                products.Add(product);
            }

            string line = $"[QueueTrigger]: {DateTime.Now} finished execution {queueMessage}. Total time to create {totalUpserts} rows={sw.ElapsedMilliseconds}.";
            log.LogInformation(line);

        }
    }
}
