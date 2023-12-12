using System.Collections.Concurrent;

namespace WebApp.Services;

/// <summary>
/// A very simple singleton memory storage thing.
/// </summary>
public class WebAppDatabase
{
    private ConcurrentDictionary<string, object> dictionary;

    public WebAppDatabase()
    {
        this.dictionary = new ConcurrentDictionary<string, object>();
    }

    public void Set<T>(string key, T val)
    {
        ArgumentNullException.ThrowIfNull(val);
        this.dictionary.TryAdd(key, val);
    }

    public T? Get<T>(string key)
    {
        this.dictionary.TryGetValue(key, out var val);
        return val != null ? (T)val : default;
    }

    public void Remove(string key)
    {
        this.dictionary.TryRemove(key, out var val);
    }
}
