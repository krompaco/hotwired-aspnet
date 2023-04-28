﻿using System.Collections.Generic;
using Krompaco.AspNetCore.Hotwired.Extensions;
using Krompaco.AspNetCore.Hotwired.TurboStreams;
using Microsoft.AspNetCore.Components.Endpoints;
using Microsoft.AspNetCore.Mvc;
using WebApp.Extensions;
using WebApp.Models;
using WebApp.Pages;

namespace WebApp.Controllers;

public class FormsController : Controller
{
    private readonly ILogger<FormsController> logger;

    public FormsController(ILogger<FormsController> logger)
    {
        this.logger = logger;
    }

    ////[HttpPost]
    ////public IResult Contact(Contact.ContactFormPostModel postModel)
    ////{
    ////    var dictionary = new Dictionary<string, object?>
    ////    {
    ////        { "FormPostModel", postModel },
    ////    };

    ////    var result = new RazorComponentResult<Contact>(dictionary.AsReadOnly());
    ////    return result;
    ////}

    ////[HttpPost]
    ////public IResult Player()
    ////{
    ////    this.logger.LogInformation("Hello from OnPost() " + this.PlayerFormModel.Id.ToString("D"));

    ////    if (!this.ModelState.IsValid)
    ////    {
    ////        // This follows the recommendation to set status = 422 for validation errors
    ////        this.Response.SetTurboValidationErrorStatus(this.Request);
    ////        var result = new RazorComponentResult<PlayerForm>(dictionary.AsReadOnly());
    ////        return result;
    ////    }

    ////    var all = this.TempData.GetPlayers();

    ////    var match = all.SingleOrDefault(x => x.Id == this.PlayerFormModel.Id);
    ////    var playerAdded = false;

    ////    if (match == null)
    ////    {
    ////        match = new PlayerFormModel { Id = this.PlayerFormModel.Id };
    ////        all.Add(match);
    ////        playerAdded = true;
    ////    }

    ////    var rankingUpdated = match.Ranking != this.PlayerFormModel.Ranking;

    ////    match.Name = this.PlayerFormModel.Name;
    ////    match.Ranking = this.PlayerFormModel.Ranking;

    ////    this.TempData.SetPlayers(all);

    ////    var updateMessage = rankingUpdated || playerAdded ? new TurboStreamMessage
    ////    {
    ////        Action = TurboStreamAction.Update,
    ////        Target = "js-player-list",
    ////        TemplateInnerHtml = await this.viewComponentToStringRenderer.RenderAsync("PlayerList", all.OrderBy(x => x.Ranking).ToList()),
    ////    }
    ////    : new TurboStreamMessage
    ////    {
    ////        Action = TurboStreamAction.Update,
    ////        Target = "js-player-list-item-" + this.PlayerFormModel.Id.ToString("D"),
    ////        TemplateInnerHtml = await this.viewComponentToStringRenderer.RenderAsync("PlayerListItem", match),
    ////    };

    ////    var alert = await this.viewComponentToStringRenderer.RenderAsync(
    ////        "Alert",
    ////        new Alert(match.Name + " was saved."));

    ////    var alertMessage = new TurboStreamMessage
    ////    {
    ////        Action = TurboStreamAction.Update,
    ////        Target = "js-alert-target",
    ////        TemplateInnerHtml = alert,
    ////    };

    ////    var removeFormMessage = new TurboStreamMessage
    ////    {
    ////        Action = TurboStreamAction.Update,
    ////        Target = "js-player-form",
    ////        TemplateInnerHtml = "<!-- Emptied after update -->",
    ////    };

    ////    return this.Content(updateMessage.ToString() + removeFormMessage + alertMessage, TurboStreamMessage.MimeType);
    ////}
}
