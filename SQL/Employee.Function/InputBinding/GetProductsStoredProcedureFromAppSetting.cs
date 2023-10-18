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
    public class GetProductsStoredProcedureFromAppSetting
    {
        /// <summary>
        /// This shows an example of a SQL Input binding that uses a stored procedure 
        /// from an app setting value to query for Products with a specific cost that is also defined as an app setting value.
        /// </summary>
        [FunctionName("GetProductsStoredProcedureFromAppSetting")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Input Binding" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(IEnumerable<Product>), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getproductsbycost")]
            HttpRequest req,
            [Sql("%Sp_SelectCost%",
                "SqlConnectionString",
                commandType: System.Data.CommandType.StoredProcedure,
                parameters: "@Cost=%ProductCost%")]
            IEnumerable<Product> products)
        {
            return new OkObjectResult(products);
        }
    }
}

