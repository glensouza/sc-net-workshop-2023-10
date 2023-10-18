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
    public class GetProductNamesView
    {
        private readonly ILogger<GetProductNamesView> _logger;

        public GetProductNamesView(ILogger<GetProductNamesView> log)
        {
            _logger = log;
        }

        [FunctionName("GetProductNamesView")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Input Binding" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IEnumerable<ProductName>), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getproduct-namesview/")]
            HttpRequest req,
            [Sql("SELECT * FROM ProductNames",
                "SqlConnectionString")]
            IEnumerable<ProductName> products)
        {
            return new OkObjectResult(products);
        }
    }
}

