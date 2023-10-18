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
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Employee.Function.InputBinding
{
    public class GetProductsSqlCommand
    {
        private readonly ILogger<GetProductsSqlCommand> _logger;

        public GetProductsSqlCommand(ILogger<GetProductsSqlCommand> log)
        {
            _logger = log;
        }

        [FunctionName("GetProductsSqlCommand")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Input Binding" })]
        [OpenApiParameter(name: "cost", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **Cost** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getproducts-sqlcommand/{cost}")]
            HttpRequest req,
            [Sql("select * from Products where cost = @Cost",
                "SqlConnectionString",
                parameters: "@Cost={cost}")]
            SqlCommand command)
        {
            string result = string.Empty;
            await using (SqlConnection connection = command.Connection)
            {
                connection.Open();
                await using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    result += $"ProductId: {reader["ProductId"]},  Name: {reader["Name"]}, Cost: {reader["Cost"]}\n";
                }
            }
            return new OkObjectResult(result);
        }
    }
}

