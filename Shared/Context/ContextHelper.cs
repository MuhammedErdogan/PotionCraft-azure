using System.Net;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;

public abstract class ContextHelper
{
    public static async Task<IContext<T>> CreateContextAsync<T>(HttpRequestData req)
    {
        var requestBody = await req.ReadAsStringAsync();
        var functionExecutionContext = JsonConvert.DeserializeObject<FunctionExecutionContext<T>>(requestBody);

        var context = new Context<T>(functionExecutionContext);
        return context;
    }


    public static async Task<HttpResponseData> CreateResponse<T>(HttpRequestData req, T body)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        if (body != null)
        {
            await response.WriteStringAsync(JsonConvert.SerializeObject(body));
        }

        return response;
    }
}