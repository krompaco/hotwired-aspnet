using Krompaco.AspNetCore.Hotwired.TurboStreams;
using Microsoft.AspNetCore.Http;

namespace Krompaco.AspNetCore.Hotwired.Extensions;

public static class HttpRequestExtensions
{
    public static bool IsGet(this HttpRequest request)
    {
        return request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsPost(this HttpRequest request)
    {
        return request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsTurbo(this HttpRequest request)
    {
        if (!request.Headers.TryGetValue("Accept", out var values))
        {
            return false;
        }

        return values.Count > 0 && (values[0]?.Contains(TurboStreamMessage.MimeType, StringComparison.OrdinalIgnoreCase) ?? false);
    }
}
