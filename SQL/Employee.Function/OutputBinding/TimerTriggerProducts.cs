using System;
using Employee.Function.Common;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Employee.Function.OutputBinding
{
    public class TimerTriggerProducts
    {
        private static int _executionNumber = 0;

        /// <summary>
        /// This timer function runs every 5 seconds, each time it upserts 1000 rows of data.
        /// </summary>
        [FunctionName("TimerTriggerProducts")]
        public static void Run(
            [TimerTrigger("*/5 * * * * *")] TimerInfo req, ILogger log,
            [Sql("Products", "SqlConnectionString")] ICollector<Product> products)
        {
            int totalUpserts = 1000;
            log.LogInformation($"{DateTime.Now} starting execution #{_executionNumber}. Rows to generate={totalUpserts}.");

            var sw = new Stopwatch();
            sw.Start();

            List<Product> newProducts = ProductUtilities.GetNewProducts(totalUpserts);
            foreach (Product product in newProducts)
            {
                products.Add(product);
            }

            sw.Stop();

            string line = $"{DateTime.Now} finished execution #{_executionNumber}. Total time to create {totalUpserts} rows={sw.ElapsedMilliseconds}.";
            log.LogInformation(line);

            _executionNumber++;
        }
    }
}
