using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Employee.Function.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Employee.Function.InputBinding
{
    public class GetProductsNameNull
    {
        // In this example, if {name} is "null", then the value attached to the @Name parameter is null.
        // This means the input binding returns all products for which the Name column is null.
        // Otherwise, {name} is interpreted as a string, and the input binding returns all products
        // for which the Name column is equal to that string value
        [FunctionName("GetProductsNameNull")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Input Binding" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Path, Required = false, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IEnumerable<Product>), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getproducts-namenull/{name}")]
            HttpRequest req,
            [Sql("if @Name is null select * from Products where Name is null else select * from Products where @Name = name",
                "SqlConnectionString",
                parameters: "@Name={name}")]
            IEnumerable<Product> products)
        {
            return new OkObjectResult(products);
        }
    }
}
