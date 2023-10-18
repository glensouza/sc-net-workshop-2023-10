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
    public class GetProductsString
    {
        private readonly ILogger<GetProductsString> _logger;

        public GetProductsString(ILogger<GetProductsString> log)
        {
            _logger = log;
        }

        [FunctionName("GetProductsString")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Input Binding" })]
        [OpenApiParameter(name: "cost", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **Cost** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getproducts-string/{cost}")]
            HttpRequest req,
            [Sql("select * from Products where cost = @Cost",
                "SqlConnectionString",
                parameters: "@Cost={cost}")]
            string products)
        {
            // Products is a JSON representation of the returned rows. For example, if there are two returned rows,
            // products could look like:
            // [{"ProductId":1,"Name":"Dress","Cost":100},{"ProductId":2,"Name":"Skirt","Cost":100}]
            return new OkObjectResult(products);
        }
    }
}
