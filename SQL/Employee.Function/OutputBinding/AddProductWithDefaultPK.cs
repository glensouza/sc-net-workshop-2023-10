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

