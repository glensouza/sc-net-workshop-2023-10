using System.Collections.Generic;
using System.IO;
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

namespace Employee.Function.InputBinding
{
    public class GetProductsTopN
    {
        private readonly ILogger<GetProductsTopN> _logger;

        public GetProductsTopN(ILogger<GetProductsTopN> log)
        {
            _logger = log;
        }

        [FunctionName("GetProductsTopN")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Input Binding" })]
        [OpenApiParameter(name: "count", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **Count** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IEnumerable<Product>), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getproductstopn/{count}")]
            HttpRequest req,
            [Sql("SELECT TOP(CAST(@Count AS INT)) * FROM Products",
                "SqlConnectionString",
                parameters: "@Count={count}")]
            IEnumerable<Product> products)
        {
            return new OkObjectResult(products);
        }
    }
}

