using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Core.Enums;
using Aliencube.AzureFunctions.FunctionApp.Models;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Aliencube.AzureFunctions.FunctionAppV1Static
{
    public static class DummyHttpTrigger
    {
        [FunctionName(nameof(DummyHttpTrigger.GetDummies))]
        [OpenApiOperation(operationId: "getDummies", tags: new[] { "dummy" }, Summary = "Gets the list of dummies", Description = "This gets the list of dummies.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "Dummy name", Description = "Dummy name", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "switch", In = ParameterLocation.Path, Required = true, Type = typeof(StringEnum), Summary = "Dummy switch", Description = "Dummy switch", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<DummyResponseModel>), Summary = "List of the dummy responses", Description = "This returns the list of dummy responses")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = "Name not found", Description = "Name parameter is not found")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid switch", Description = "Switch parameter is not valid")]
        public static async Task<HttpResponseMessage> GetDummies(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "dummies")] HttpRequestMessage req,
            ILogger log)
        {
            var content = new List<DummyResponseModel>()
            {
                new DummyResponseModel(),
            };
            var result = req.CreateResponse(HttpStatusCode.OK, content);

            return await Task.FromResult(result).ConfigureAwait(false);
        }

        [FunctionName(nameof(DummyHttpTrigger.AddDummy))]
        [OpenApiOperation(operationId: "addDummy", tags: new[] { "dummy" }, Summary = "Adds a dummy", Description = "This adds a dummy.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(DummyRequestModel), Required = true, Description = "Dummy request model")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(DummyResponseModel), Summary = "Dummy response", Description = "This returns the dummy response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid request payload", Description = "Request payload is not valid")]
        public static async Task<HttpResponseMessage> AddDummy(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "dummies")] HttpRequestMessage req,
            ILogger log)
        {
            var content = new DummyResponseModel();
            var result = req.CreateResponse(HttpStatusCode.OK, content);

            return await Task.FromResult(result).ConfigureAwait(false);
        }

        [FunctionName(nameof(DummyHttpTrigger.UpdateDummies))]
        [OpenApiOperation(operationId: "updateDummies", tags: new[] { "dummy" }, Summary = "Updates a list of dummies", Description = "This updates a list of dummies.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(DummyListModel), Required = true, Description = "Dummy list model")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<DummyStringModel>), Summary = "Dummy response", Description = "This returns the dummy response")]
        public static async Task<HttpResponseMessage> UpdateDummies(
            [HttpTrigger(AuthorizationLevel.Function, "PUT", Route = "dummies")] HttpRequestMessage req,
            ILogger log)
        {
            var content = new List<DummyStringModel>();
            var result = req.CreateResponse(HttpStatusCode.OK, content);

            return await Task.FromResult(result).ConfigureAwait(false);
        }
    }
}
