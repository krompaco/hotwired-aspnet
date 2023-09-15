using Krompaco.AspNetCore.Hotwired.TurboStreams;
using Microsoft.AspNetCore.Mvc;
using WebApp.Components.Pages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

public class FormsController : Controller
{
    private readonly ILogger<FormsController> logger;

    private readonly ILoggerFactory loggerFactory;

    private readonly WebAppDatabase webAppDatabase;

    private readonly SiteComponentRenderer componentRenderer;

    public FormsController(ILogger<FormsController> logger, ILoggerFactory loggerFactory, WebAppDatabase webAppDatabase, SiteComponentRenderer componentRenderer)
    {
        this.logger = logger;
        this.loggerFactory = loggerFactory;
        this.webAppDatabase = webAppDatabase;
        this.componentRenderer = componentRenderer;
    }

    // An example of how to work from a controller action
    [HttpPost]
    public async Task<IActionResult> DeletePlayer([FromForm]string id)
    {
        this.logger.LogInformation("Hello from FormsController.DeletePlayer() " + id);

        var sessionKey = this.HttpContext.Session.Id + nameof(PlayerList);

        var all = this.webAppDatabase.Get<List<PlayerFormModel>>(sessionKey);
        var match = all.SingleOrDefault(x => x.Id == id);

        if (match != null)
        {
            all.Remove(match);
            this.webAppDatabase.Set<List<PlayerFormModel>>(sessionKey, all);

            var removeMessage = new TurboStreamMessage
            {
                Action = TurboStreamAction.Remove,
                Target = "js-player-list-item-" + id,
            };

            var alertModel = new Alert(match.Name + " was deleted.", AlertType.Information);
            var alertHtml = await this.componentRenderer.GetAlertAsHtmlAsync(alertModel);

            var alertMessage = new TurboStreamMessage
            {
                Action = TurboStreamAction.Update,
                Target = "js-alert-target",
                TemplateInnerHtml = alertHtml,
            };

            return this.Content(removeMessage + alertMessage.ToString(), TurboStreamMessage.MimeType);
        }

        return this.NotFound();
    }
}
