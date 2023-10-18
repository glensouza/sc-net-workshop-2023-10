using System.Net;
using Employee.Function.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace Employee.Function.OutputBinding
{
    public class AddProduct
    {
        [FunctionName("AddProduct")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Output Binding" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Product), Description = "Product Parameter", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "addproduct")]
            [FromBody] Product prod,
            [Sql("dbo.Products", "SqlConnectionString")] out Product product)
        {
            product = prod;
            return new CreatedResult($"/api/addproduct", product);
        }
    }
}
