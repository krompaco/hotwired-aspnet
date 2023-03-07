using Krompaco.AspNetCore.Hotwired.Extensions;
using Krompaco.AspNetCore.Hotwired.TurboStreams;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Pages;

public class PlayerFormPageModel : PageModel
{
    private readonly ILogger<IndexModel> logger;

    private readonly RazorViewComponentToStringRenderer viewComponentToStringRenderer;

    public PlayerFormPageModel(ILogger<IndexModel> logger, RazorViewComponentToStringRenderer viewComponentToStringRenderer)
    {
        this.logger = logger;
        this.viewComponentToStringRenderer = viewComponentToStringRenderer;
        this.PlayerFormModel = new PlayerFormModel();
        this.Id = Guid.Empty;
    }

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public PlayerFormModel PlayerFormModel { get; set; }

    public void OnGet()
    {
        this.logger.LogInformation("Hello from OnGet() " + this.Id.ToString("D"));

        var all = this.TempData.GetPlayers();
        var match = all.SingleOrDefault(x => x.Id == this.Id);

        if (match == null)
        {
            this.PlayerFormModel.Id = Guid.NewGuid();
            return;
        }

        this.PlayerFormModel.Id = match.Id;
        this.PlayerFormModel.Name = match.Name;
        this.PlayerFormModel.Ranking = match.Ranking;
    }

    public async Task<IActionResult> OnPost()
    {
        this.logger.LogInformation("Hello from OnPost() " + this.PlayerFormModel.Id.ToString("D"));

        if (!this.ModelState.IsValid)
        {
            // This follows the recommendation to set status = 422 for validation errors
            this.Response.SetTurboValidationErrorStatus(this.Request);
            return this.Page();
        }

        var all = this.TempData.GetPlayers();

        var match = all.SingleOrDefault(x => x.Id == this.PlayerFormModel.Id);
        var playerAdded = false;

        if (match == null)
        {
            match = new PlayerFormModel { Id = this.PlayerFormModel.Id };
            all.Add(match);
            playerAdded = true;
        }

        var rankingUpdated = match.Ranking != this.PlayerFormModel.Ranking;

        match.Name = this.PlayerFormModel.Name;
        match.Ranking = this.PlayerFormModel.Ranking;

        this.TempData.SetPlayers(all);

        var updateMessage = rankingUpdated || playerAdded ? new TurboStreamMessage
        {
            Action = TurboStreamAction.Update,
            Target = "js-player-list",
            TemplateInnerHtml = await this.viewComponentToStringRenderer.RenderAsync("PlayerList", all.OrderBy(x => x.Ranking).ToList()),
        }
        : new TurboStreamMessage
        {
            Action = TurboStreamAction.Update,
            Target = "js-player-list-item-" + this.PlayerFormModel.Id.ToString("D"),
            TemplateInnerHtml = await this.viewComponentToStringRenderer.RenderAsync("PlayerListItem", match),
        };

        var alert = await this.viewComponentToStringRenderer.RenderAsync(
            "Alert",
            new Alert(match.Name + " was saved."));

        var alertMessage = new TurboStreamMessage
        {
            Action = TurboStreamAction.Update,
            Target = "js-alert-target",
            TemplateInnerHtml = alert,
        };

        var removeFormMessage = new TurboStreamMessage
        {
            Action = TurboStreamAction.Update,
            Target = "js-player-form",
            TemplateInnerHtml = "<!-- Emptied after update -->",
        };

        return this.Content(updateMessage.ToString() + removeFormMessage + alertMessage, TurboStreamMessage.MimeType);
    }
}
