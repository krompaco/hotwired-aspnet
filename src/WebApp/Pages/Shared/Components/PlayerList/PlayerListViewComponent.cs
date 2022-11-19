using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Pages.Shared.Components.PlayerList;

public class PlayerListViewComponent : ViewComponent
{
#pragma warning disable CS1998
    public async Task<IViewComponentResult> InvokeAsync(List<PlayerFormModel> players)
#pragma warning restore CS1998
    {
        return this.View(nameof(PlayerListViewComponent), players);
    }
}
