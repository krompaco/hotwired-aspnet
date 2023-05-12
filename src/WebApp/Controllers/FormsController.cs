using Krompaco.AspNetCore.Hotwired.TurboStreams;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Endpoints;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApp.Hubs;
using WebApp.Models;
using WebApp.Pages;

namespace WebApp.Controllers;

public class FormsController : Controller
{
    private readonly ILogger<FormsController> logger;

    private readonly ILoggerFactory loggerFactory;

    private readonly IHubContext<AppHub> hub;

    public FormsController(ILogger<FormsController> logger, ILoggerFactory loggerFactory, IHubContext<AppHub> hub)
    {
        this.logger = logger;
        this.loggerFactory = loggerFactory;
        this.hub = hub;
    }

    [HttpPost]
    public async Task<IResult> GlobalMessage(GlobalMessageFormModel postModel)
    {
        this.logger.LogInformation("Hello from GlobalMessage()");

        var dictionary = new Dictionary<string, object?>
        {
            { "GlobalMessageFormModel", postModel },
        };

        var result = new RazorComponentResult<GlobalMessageForm>(dictionary.AsReadOnly());

        ////if (!this.ModelState.IsValid)
        ////{
        ////    // This follows the recommendation to set status = 422 for validation errors
        ////    this.Response.SetTurboValidationErrorStatus(this.Request);
        ////    return result;
        ////}

        var alertModel = new Models.Alert(postModel.Message!);
        var alertDictionary = new Dictionary<string, object?>
                                {
                                    { "AlertModel", alertModel },
                                };
        var html = await this.GetAsHtmlAsync<Shared.Alert>(alertDictionary);

        var alertMessage = new TurboStreamMessage
        {
            Action = TurboStreamAction.Update,
            Target = "js-alert-target",
            TemplateInnerHtml = html,
        };

        await this.hub.Clients.All.SendAsync("GlobalMessageReceived", alertMessage.ToString());
        return result;
    }

    // Example of responding with rendered Razor Component from a Controller or Razor Page model:
    ////[HttpPost]
    ////public IResult Contact(Contact.ContactFormPostModel postModel)
    ////{
    ////    var dictionary = new Dictionary<string, object?>
    ////    {
    ////        { "FormPostModel", postModel },
    ////    };

    ////    var result = new RazorComponentResult<Contact>(dictionary.AsReadOnly());
    ////    return result;
    ////}

    ////[HttpPost]
    ////public IResult Player()
    ////{
    ////    this.logger.LogInformation("Hello from OnPost() " + this.PlayerFormModel.Id.ToString("D"));

    ////    if (!this.ModelState.IsValid)
    ////    {
    ////        // This follows the recommendation to set status = 422 for validation errors
    ////        this.Response.SetTurboValidationErrorStatus(this.Request);
    ////        var result = new RazorComponentResult<PlayerForm>(dictionary.AsReadOnly());
    ////        return result;
    ////    }

    ////    var all = this.TempData.GetPlayers();

    ////    var match = all.SingleOrDefault(x => x.Id == this.PlayerFormModel.Id);
    ////    var playerAdded = false;

    ////    if (match == null)
    ////    {
    ////        match = new PlayerFormModel { Id = this.PlayerFormModel.Id };
    ////        all.Add(match);
    ////        playerAdded = true;
    ////    }

    ////    var rankingUpdated = match.Ranking != this.PlayerFormModel.Ranking;

    ////    match.Name = this.PlayerFormModel.Name;
    ////    match.Ranking = this.PlayerFormModel.Ranking;

    ////    this.TempData.SetPlayers(all);

    ////    var updateMessage = rankingUpdated || playerAdded ? new TurboStreamMessage
    ////    {
    ////        Action = TurboStreamAction.Update,
    ////        Target = "js-player-list",
    ////        TemplateInnerHtml = await this.viewComponentToStringRenderer.RenderAsync("PlayerList", all.OrderBy(x => x.Ranking).ToList()),
    ////    }
    ////    : new TurboStreamMessage
    ////    {
    ////        Action = TurboStreamAction.Update,
    ////        Target = "js-player-list-item-" + this.PlayerFormModel.Id.ToString("D"),
    ////        TemplateInnerHtml = await this.viewComponentToStringRenderer.RenderAsync("PlayerListItem", match),
    ////    };

    ////    var alert = await this.viewComponentToStringRenderer.RenderAsync(
    ////        "Alert",
    ////        new Alert(match.Name + " was saved."));

    ////    var alertMessage = new TurboStreamMessage
    ////    {
    ////        Action = TurboStreamAction.Update,
    ////        Target = "js-alert-target",
    ////        TemplateInnerHtml = alert,
    ////    };

    ////    var removeFormMessage = new TurboStreamMessage
    ////    {
    ////        Action = TurboStreamAction.Update,
    ////        Target = "js-player-form",
    ////        TemplateInnerHtml = "<!-- Emptied after update -->",
    ////    };

    ////    return this.Content(updateMessage.ToString() + removeFormMessage + alertMessage, TurboStreamMessage.MimeType);
    ////}

    private async Task<string> GetAsHtmlAsync<T>(Dictionary<string, object?> dictionary)
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
