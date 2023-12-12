using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WebApp.Components;

namespace WebApp.Services;

public class SiteComponentRenderer
{
    private readonly ILoggerFactory loggerFactory;

    private readonly IHttpContextAccessor httpContextAccessor;

    public SiteComponentRenderer(ILoggerFactory loggerFactory, IHttpContextAccessor httpContextAccessor)
    {
        this.loggerFactory = loggerFactory;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> GetAlertAsHtmlAsync(Models.Alert alert)
    {
        var alertDictionary = new Dictionary<string, object?>
        {
            { "AlertModel", alert },
        };
        var alertHtml = await this.GetAsHtmlAsync<AlertComponent>(alertDictionary);
        return alertHtml;
    }

    public async Task<string> GetAsHtmlAsync<T>(Dictionary<string, object?> dictionary)
        where T : IComponent
    {
        var parameters = ParameterView.FromDictionary(dictionary);

        await using var htmlRenderer = new HtmlRenderer(this.httpContextAccessor.HttpContext!.RequestServices, this.loggerFactory);
        var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var output = await htmlRenderer.RenderComponentAsync<T>(parameters);
            return output.ToHtmlString();
        });
        return html;
    }
}
