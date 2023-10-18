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
    public class GetProductsStoredProcedure
    {
        private readonly ILogger<GetProductsStoredProcedure> _logger;

        public GetProductsStoredProcedure(ILogger<GetProductsStoredProcedure> log)
        {
            _logger = log;
        }

        [FunctionName("GetProductsStoredProcedure")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Input Binding" })]
        [OpenApiParameter(name: "cost", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **Cost** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IEnumerable<Product>), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getproducts-storedprocedure/{cost}")]
            HttpRequest req,
            [Sql("SelectProductsCost",
                "SqlConnectionString",
                commandType: System.Data.CommandType.StoredProcedure,
                parameters: "@Cost={cost}")]
            IEnumerable<Product> products)
        {
            return new OkObjectResult(products);
        }
    }
}

