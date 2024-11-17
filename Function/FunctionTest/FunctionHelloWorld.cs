using DomainTest;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedTest;

namespace FunctionTest;

public class FunctionHelloWorld(ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<FunctionHelloWorld>();

    [Function("FunctionHelloWorld")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req, FunctionContext executionContext)
    {
        _logger.LogInformation("===== FunctionAttendanceCheck =====");
        var context = await ContextHelper.CreateContextAsync<HelloWorldRequest>(req);
        _logger.LogInformation($"FunctionAttendanceCheck: {context}");

        IHelloWorldAsync helloWorldAsync = new HelloWorldAsync();
        var helloWorldResult = await helloWorldAsync.HelloWorld(context.PlayFabId, context.Request);

        _logger.LogInformation($"helloWorldResult: {JsonConvert.SerializeObject(helloWorldResult)}");

        var response = await ContextHelper.CreateResponse<HelloWorldResult>(req, helloWorldResult);

        return response;
    }
}