using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WebApp.Models;

namespace WebApp.Extensions;

public static class ITempDataDictionaryExtensions
{
    private const string PlayersKey = "Players";

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

    public static List<PlayerFormModel> GetPlayers(this ITempDataDictionary tempData)
    {
        var all = tempData.Get<List<PlayerFormModel>>(PlayersKey) ?? new List<PlayerFormModel>();
        tempData.Keep(PlayersKey);
        return all;
    }

    public static void SetPlayers(this ITempDataDictionary tempData, List<PlayerFormModel> all)
    {
        tempData.Set(PlayersKey, all);
    }
}
