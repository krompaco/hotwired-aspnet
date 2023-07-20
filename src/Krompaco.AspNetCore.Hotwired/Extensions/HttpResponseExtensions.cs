using Microsoft.AspNetCore.Http;

namespace Krompaco.AspNetCore.Hotwired.Extensions;

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

    public static void SetTurboRedirectStatus(this HttpResponse response, string location)
    {
        response.Headers.Location = location;
        response.StatusCode = 303;
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
