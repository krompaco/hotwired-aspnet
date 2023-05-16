using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WebApp.Services;

public class SiteComponentRenderer
{
    private readonly ILoggerFactory loggerFactory;

    public SiteComponentRenderer(ILoggerFactory loggerFactory)
    {
        this.loggerFactory = loggerFactory;
    }

    public async Task<string> GetAsHtmlAsync<T>(Dictionary<string, object?> dictionary)
        where T : IComponent
    {
        var parameters = ParameterView.FromDictionary(dictionary);
        await using var htmlRenderer = new HtmlRenderer(Program.ServiceProvider, this.loggerFactory);
        var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var output = await htmlRenderer.RenderComponentAsync<T>(parameters);
            return output.ToHtmlString();
        });
        return html;
    }
}
