using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Pages.Shared.Components.PlayerListItem;

public class SpinnerViewComponent : ViewComponent
{
#pragma warning disable CS1998
    public async Task<IViewComponentResult> InvokeAsync()
#pragma warning restore CS1998
    {
        return this.View(nameof(SpinnerViewComponent));
    }
}
