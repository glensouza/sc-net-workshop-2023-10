using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Employee.Function.Common;
using Microsoft.Azure.WebJobs.Extensions.Sql;
using System.Collections.Generic;

namespace Employee.Function.TriggerBinding
{
    public static class ProductsTriggerLeasesTableName
    {
        //[FunctionName(nameof(ProductsTriggerLeasesTableName))]
        //public static void Run(
        //    [SqlTrigger("[dbo].[Products]", "SqlConnectionString", "Leases")]
        //    IReadOnlyList<SqlChange<Product>> changes,
        //    ILogger logger)
        //{
        //    logger.LogInformation("SQL Changes: " + JsonConvert.SerializeObject(changes));
        //}
    }
}
