using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Krompaco.HotwiredAspNet.Extensions;

public static class HttpResponseExtensions
{
    public static void SetNoCache(this HttpResponse response)
    {
        response.GetTypedHeaders().CacheControl =
            new Microsoft.Net.Http.Headers.CacheControlHeaderValue
            {
                NoCache = true,
                NoStore = true,
                MaxAge = TimeSpan.Zero,
            };
    }

    public static StatusCodeResult TurboRedirectStatusResult(this HttpResponse response, string location)
    {
        response.Headers.Location = location;
        return new StatusCodeResult(303);
    }

    public static void SetTurboValidationErrorStatus(this HttpResponse response, HttpRequest request)
    {
        if (!request.IsTurbo())
        {
            return;
        }

        response.StatusCode = 422;
    }
}
