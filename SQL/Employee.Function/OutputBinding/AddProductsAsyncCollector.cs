using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Employee.Function.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace Employee.Function.OutputBinding
{
    public class AddProductsAsyncCollector
    {
        private readonly ILogger<AddProductsAsyncCollector> _logger;

        public AddProductsAsyncCollector(ILogger<AddProductsAsyncCollector> log)
        {
            _logger = log;
        }

        [FunctionName("AddProductsAsyncCollector")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Output Binding" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "addproducts-asynccollector")]
            HttpRequest req,
            [Sql("dbo.Products", "SqlConnectionString")] IAsyncCollector<Product> products)
        {
            List<Product> newProducts = ProductUtilities.GetNewProducts(5000);
            foreach (Product product in newProducts)
            {
                await products.AddAsync(product);
            }
            // Rows are upserted here
            await products.FlushAsync();

            newProducts = ProductUtilities.GetNewProducts(5000);
            foreach (Product product in newProducts)
            {
                await products.AddAsync(product);
            }
            return new CreatedResult($"/api/addproducts-collector", "done");
        }
    }
}

