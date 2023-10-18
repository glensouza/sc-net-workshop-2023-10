using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Employee.Function.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Employee.Function.MultiBinding
{
    public class GetAndAddProducts
    {
        private readonly ILogger<GetAndAddProducts> _logger;

        public GetAndAddProducts(ILogger<GetAndAddProducts> log)
        {
            _logger = log;
        }

        [FunctionName("GetAndAddProducts")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Multiple Binding" })]
        [OpenApiParameter(name: "cost", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **Cost** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IEnumerable<Product>), Description = "The OK response")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getandaddproducts/{cost}")]
            HttpRequest req,
            [Sql("SELECT * FROM Products",
                "SqlConnectionString",
                parameters: "@Cost={cost}")]
            IEnumerable<Product> products,
            [Sql("ProductsWithIdentity",
                "SqlConnectionString")]
            out Product[] productsWithIdentity)
        {
            productsWithIdentity = products.ToArray();

            return new OkObjectResult(products);
        }
    }
}

