using Krompaco.AspNetCore.Hotwired.TurboStreams;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Pages;

public class PlayerListPageModel : PageModel
{
    private readonly ILogger<IndexModel> logger;

    private readonly RazorViewComponentToStringRenderer viewComponentToStringRenderer;

    public PlayerListPageModel(ILogger<IndexModel> logger, RazorViewComponentToStringRenderer viewComponentToStringRenderer)
    {
        this.logger = logger;
        this.viewComponentToStringRenderer = viewComponentToStringRenderer;

        this.AllPlayers = new List<PlayerFormModel>();
    }

    public List<PlayerFormModel> AllPlayers { get; set; }

    public void OnGet()
    {
        this.logger.LogInformation("Hello from OnGet()");
        this.SetView();
    }

    public async Task<ActionResult> OnPostDelete(Guid id)
    {
        this.logger.LogInformation("Hello from OnPostDelete() " + id.ToString("D"));

        var all = this.TempData.GetPlayers();
        var match = all.SingleOrDefault(x => x.Id == id);

        if (match != null)
        {
            all.Remove(match);
            this.TempData.SetPlayers(all);

            var removeMessage = new TurboStreamMessage
            {
                Action = TurboStreamAction.Remove,
                Target = "js-player-list-item-" + id.ToString("D"),
            };

            var alert = await this.viewComponentToStringRenderer.RenderAsync(
                "Alert",
                new Alert(match.Name + " was deleted.", AlertType.Information));

            var alertMessage = new TurboStreamMessage
            {
                Action = TurboStreamAction.Update,
                Target = "js-alert-target",
                TemplateInnerHtml = alert,
            };

            return this.Content(removeMessage + alertMessage.ToString(), TurboStreamMessage.MimeType);
        }

        return this.NotFound();
    }

    private void SetView()
    {
        this.AllPlayers = this.TempData.GetPlayers().OrderBy(x => x.Ranking).ToList();
    }
}
