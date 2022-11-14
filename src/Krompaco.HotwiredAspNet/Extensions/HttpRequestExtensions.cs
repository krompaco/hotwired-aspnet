﻿using Krompaco.HotwiredAspNet.TurboStreams;
using Microsoft.AspNetCore.Http;

namespace Krompaco.HotwiredAspNet.Extensions;

public static class HttpRequestExtensions
{
    public static bool IsTurbo(this HttpRequest request)
    {
        if (!request.Headers.TryGetValue("Accept", out var values))
        {
            return false;
        }

        return values.Count > 0 && (values[0]?.Contains(TurboStreamMessage.MimeType, StringComparison.OrdinalIgnoreCase) ?? false);
    }
}
