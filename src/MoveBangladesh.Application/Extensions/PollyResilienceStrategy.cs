using System.Net;
using Polly;
using Polly.Extensions.Http;

namespace MoveBangladesh.Application.Extensions;

public static class PollyResilienceStrategy
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError() // HttpRequestException, 5XX and 408
            .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
            .WaitAndRetryAsync(3,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt) / 2.0));
    }
}
