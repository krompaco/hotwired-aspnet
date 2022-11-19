using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Pages.Shared.Components.PlayerListItem;

public class PlayerListItemViewComponent : ViewComponent
{
#pragma warning disable CS1998
    public async Task<IViewComponentResult> InvokeAsync(PlayerFormModel player)
#pragma warning restore CS1998
    {
        return this.View(nameof(PlayerListItemViewComponent), player);
    }
}
