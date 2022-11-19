using Microsoft.AspNetCore.Mvc;

namespace WebApp.Pages.Shared.Components.Alert;

public class AlertViewComponent : ViewComponent
{
#pragma warning disable CS1998
    public async Task<IViewComponentResult> InvokeAsync(Models.Alert alert)
#pragma warning restore CS1998
    {
        return this.View(nameof(AlertViewComponent), alert);
    }
}
