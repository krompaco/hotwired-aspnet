using Krompaco.HotwiredAspNet.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        this.logger = logger;
        this.IndexForm = new IndexFormModel();
    }

    [BindProperty]
    public IndexFormModel IndexForm { get; set; }

    public IndexFormModel? TempStoredForm { get; set; }

    public void OnGet()
    {
        this.logger.LogInformation("Hello from OnGet()");

        this.TempStoredForm = this.TempData.Get<IndexFormModel>(nameof(this.IndexForm));
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

        this.TempData.Set(nameof(this.IndexForm), this.IndexForm);

        // This redirects with 303 which is recommended for both Turbo page and frame posts
        return this.Response.TurboRedirectStatusResult("/");
    }
}
