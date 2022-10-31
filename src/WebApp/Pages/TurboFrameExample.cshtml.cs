using Krompaco.HotwiredAspNet.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Pages;

public class TurboFrameExampleModel : PageModel
{
    private readonly ILogger<IndexModel> logger;

    public TurboFrameExampleModel(ILogger<IndexModel> logger)
    {
        this.logger = logger;
        this.TurboFrameExampleForm = new TurboFrameExampleFormModel();
    }

    [BindProperty]
    public TurboFrameExampleFormModel TurboFrameExampleForm { get; set; }

    public TurboFrameExampleFormModel? TempStoredForm { get; set; }

    public void OnGet()
    {
        this.logger.LogInformation("Hello from OnGet()");

        this.TempStoredForm = this.TempData.Get<TurboFrameExampleFormModel>(nameof(this.TurboFrameExampleForm));
    }

    public IActionResult OnPost()
    {
        this.logger.LogInformation("Hello from OnPost()");

        if (!this.ModelState.IsValid)
        {
            // This follows the recommendation to set status = 422 for validation errors
            this.Response.SetTurboValidationErrorStatus(this.Request);
            return this.Page();
        }

        this.TempData.Set(nameof(this.TurboFrameExampleForm), this.TurboFrameExampleForm);

        // This redirects with 303 which is recommended for both Turbo page and frame posts
        return this.Response.TurboRedirectStatusResult("/TurboFrameExample");
    }
}
