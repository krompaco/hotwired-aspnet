using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebApp.Extensions;

public static class ITempDataDictionaryExtensions
{
    public static void Set<T>(this ITempDataDictionary tempData, string key, T value)
        where T : class
    {
        tempData[key] = JsonSerializer.Serialize(value);
    }

    public static T? Get<T>(this ITempDataDictionary tempData, string key)
        where T : class
    {
        tempData.TryGetValue(key, out var o);
        return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
    }
}
