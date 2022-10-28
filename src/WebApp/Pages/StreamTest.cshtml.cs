using System;
using System.Threading.Tasks;
using Krompaco.HotwiredAspNet.TurboStreams;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class StreamTestModel : PageModel
{
    private readonly StreamTestHandler streamTestHandler;

    public StreamTestModel(StreamTestHandler streamTestHandler)
    {
        this.streamTestHandler = streamTestHandler;
    }

    public async Task OnGetAsync()
    {
        var message = new TurboStreamMessage
        {
            Action = TurboStreamAction.Update,
            Target = "stream-test",
            TemplateInnerHtml = $"Replaced {DateTime.Now:T} from StreamTest page by web socket message.",
        };

        await this.streamTestHandler.SendMessageToAllAsync(message.ToString());
    }
}
