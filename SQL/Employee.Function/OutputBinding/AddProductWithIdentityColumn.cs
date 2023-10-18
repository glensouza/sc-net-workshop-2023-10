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
    public class AddProductWithIdentityColumn
    {
        /// <summary>
        /// This shows an example of a SQL Output binding where the target table has a primary key
        /// which is an identity column. In such a case the primary key is not required to be in
        /// the object used by the binding - it will insert a row with the other values and the
        /// ID will be generated upon insert.
        /// </summary>
        /// <param name="req">The original request that triggered the function</param>
        /// <param name="product">The created Product object</param>
        /// <returns>The CreatedResult containing the new object that was inserted</returns>
        [FunctionName("AddProductWithIdentityColumn")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Output Binding" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiParameter(name: "cost", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Cost** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ProductWithoutId), Description = "The OK response")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "addproductwithidentitycolumn")]
            HttpRequest req,
            [Sql("dbo.ProductsWithIdentity", "SqlConnectionString")] out ProductWithoutId product)
        {
            product = new ProductWithoutId
            {
                Name = req.Query["name"],
                Cost = int.Parse(req.Query["cost"])
            };
            return new CreatedResult($"/api/addproductwithidentitycolumn", product);
        }
    }
}

