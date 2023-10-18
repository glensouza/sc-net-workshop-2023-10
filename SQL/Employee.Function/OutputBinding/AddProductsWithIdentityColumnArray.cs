using System.Net;
using Employee.Function.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace Employee.Function.OutputBinding
{
    public class AddProductsWithIdentityColumnArray
    {
        /// <summary>
        /// This shows an example of a SQL Output binding where the target table has a primary key
        /// which is an identity column. In such a case the primary key is not required to be in
        /// the object used by the binding - it will insert a row with the other values and the
        /// ID will be generated upon insert.
        /// </summary>
        /// <param name="req">The original request that triggered the function</param>
        /// <param name="products">The created Product array</param>
        /// <returns>The CreatedResult containing the new object that was inserted</returns>
        [FunctionName("AddProductsWithIdentityColumnArray")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Output Binding" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Product[]), Description = "The OK response")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]
            HttpRequest req,
            [Sql("dbo.ProductsWithIdentity", "SqlConnectionString")] out Product[] products)
        {
            products = new[]
            {
                new Product
                {
                    Name = "Cup",
                    Cost = 2
                },
                new Product
                {
                    Name = "Glasses",
                    Cost = 12
                }
            };
            return new CreatedResult($"/api/addproductswithidentitycolumnarray", products);
        }
    }
}

