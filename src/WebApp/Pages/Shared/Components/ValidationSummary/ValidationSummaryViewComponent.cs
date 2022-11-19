using Microsoft.AspNetCore.Mvc;

namespace WebApp.Pages.Shared.Components.ValidationSummary;

public class ValidationSummaryViewComponent : ViewComponent
{
#pragma warning disable CS1998
    public async Task<IViewComponentResult> InvokeAsync(Microsoft.AspNetCore.Mvc.RazorPages.PageModel pageModel)
#pragma warning restore CS1998
    {
        return this.View(nameof(ValidationSummaryViewComponent), pageModel);
    }
}
