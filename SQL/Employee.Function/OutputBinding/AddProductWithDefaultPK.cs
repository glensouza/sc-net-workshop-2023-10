using System.Net;
using Employee.Function.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace Employee.Function.OutputBinding
{
    public class AddProductWithDefaultPK
    {
        [FunctionName("AddProductWithDefaultPK")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Output Binding" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ProductWithDefaultPK), Description = "Product Parameter", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ProductWithDefaultPK), Description = "The OK response")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "addproductwithdefaultpk")]
            [FromBody] ProductWithDefaultPK product,
            [Sql("dbo.ProductsWithDefaultPK", "SqlConnectionString")] out ProductWithDefaultPK output)
        {
            output = product;
            return new CreatedResult($"/api/addproductwithdefaultpk", output);
        }
    }
}

