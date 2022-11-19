using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class PlayerFormEmptyPageModel : PageModel
{
    private readonly ILogger<IndexModel> logger;

    public PlayerFormEmptyPageModel(ILogger<IndexModel> logger)
    {
        this.logger = logger;
    }

    public void OnGet()
    {
        this.logger.LogInformation("Hello from OnGet()");
    }
}
