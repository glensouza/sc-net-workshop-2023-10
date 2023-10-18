using System.Collections.Generic;
using System.Net;
using Employee.Function.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace Employee.Function.OutputBinding
{
    public class AddProductsArray
    {
        [FunctionName("AddProductsArray")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Output Binding" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(List<Product>), Description = "Product Parameter", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "addproducts-array")]
            [FromBody] List<Product> products,
            [Sql("dbo.Products", "SqlConnectionString")] out Product[] output)
        {
            // Upsert the products, which will insert them into the Products table if the primary key (ProductId) for that item doesn't exist. 
            // If it does then update it to have the new name and cost
            output = products.ToArray();
            return new CreatedResult($"/api/addproducts-array", output);
        }
    }
}
