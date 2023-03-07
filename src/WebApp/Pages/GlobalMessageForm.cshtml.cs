using System.Text.RegularExpressions;
using Krompaco.AspNetCore.Hotwired.Extensions;
using Krompaco.AspNetCore.Hotwired.TurboStreams;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using WebApp.Extensions;
using WebApp.Hubs;
using WebApp.Models;

namespace WebApp.Pages;

public class GlobalMessageFormPageModel : PageModel
{
    private readonly ILogger<IndexModel> logger;

    private readonly RazorViewComponentToStringRenderer viewComponentToStringRenderer;

    private readonly IHubContext<AppHub> hub;

    public GlobalMessageFormPageModel(ILogger<IndexModel> logger, RazorViewComponentToStringRenderer viewComponentToStringRenderer, IHubContext<AppHub> hub)
    {
        this.logger = logger;
        this.viewComponentToStringRenderer = viewComponentToStringRenderer;
        this.hub = hub;
        this.GlobalMessageFormModel = new GlobalMessageFormModel();
    }

    [BindProperty]
    public GlobalMessageFormModel GlobalMessageFormModel { get; set; }

    public void OnGet()
    {
        this.logger.LogInformation("Hello from OnGet()");
    }

    public async Task<IActionResult> OnPost()
    {
        this.logger.LogInformation("Hello from OnPost()");

        if (!this.ModelState.IsValid)
        {
            // This follows the recommendation to set status = 422 for validation errors
            this.Response.SetTurboValidationErrorStatus(this.Request);
            return this.Page();
        }

        var alert = await this.viewComponentToStringRenderer.RenderAsync(
            "Alert",
            new Alert(this.GlobalMessageFormModel.Message!));

        var alertMessage = new TurboStreamMessage
        {
            Action = TurboStreamAction.Update,
            Target = "js-alert-target",
            TemplateInnerHtml = alert,
        };

        await this.hub.Clients.All.SendAsync("GlobalMessageReceived", alertMessage.ToString());
        return new EmptyResult();
    }
}
