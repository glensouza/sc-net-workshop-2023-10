using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Employee.Function.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace Employee.Function.InputBinding
{
    public class GetProductNamesView
    {
        /// <summary>
        /// This shows an example of a SQL Input binding that queries from a SQL View named ProductNames.
        /// </summary>
        [FunctionName("GetProductNamesView")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Input Binding" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IEnumerable<ProductName>), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getproduct-namesview/")]
            HttpRequest req,
            [Sql("SELECT * FROM ProductNames", "SqlConnectionString")] IEnumerable<ProductName> products)
        {
            return new OkObjectResult(products);
        }
    }
}

