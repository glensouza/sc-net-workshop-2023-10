using System.Net;
using Employee.Function.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Employee.Function.OutputBinding
{
    public class AddProductParams
    {
        [FunctionName("AddProductParams")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Output Binding" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiParameter(name: "productId", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **ProductId** parameter")]
        [OpenApiParameter(name: "cost", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Cost** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "addproduct-params")]
            HttpRequest req,
            [Sql("dbo.Products", "SqlConnectionString")] out Product product)
        {
            product = new Product
            {
                Name = req.Query["name"],
                ProductId = int.Parse(req.Query["productId"]),
                Cost = int.Parse(req.Query["cost"])
            };
            return new CreatedResult($"/api/addproduct", product);
        }
    }
}

