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

namespace Employee.Function.OutputBinding
{
    public class AddProductsCollector
    {
        [FunctionName("AddProductsCollector")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Output Binding" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "addproducts-collector")]
            HttpRequest req,
            [Sql("dbo.Products", "SqlConnectionString")] ICollector<Product> products)
        {
            List<Product> newProducts = ProductUtilities.GetNewProducts(5000);
            foreach (Product product in newProducts)
            {
                products.Add(product);
            }
            return new CreatedResult($"/api/addproducts-collector", "done");
        }
    }
}
