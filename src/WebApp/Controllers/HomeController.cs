using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Extensions;
using WebApp.Models;
using WebApp.TurboStreams;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly StreamTestHandler streamTestHandler;

        private readonly IHttpContextAccessor httpContextAccessor;

        public HomeController(StreamTestHandler streamTestHandler, IHttpContextAccessor httpContextAccessor)
        {
            this.streamTestHandler = streamTestHandler;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // TODO: Fix better way to give visitor a SessionId
            this.httpContextAccessor.HttpContext?.Session.GetOrCreateSessionId();

            return this.View();
        }

        [HttpPost]
        public IActionResult Index(string mainBody)
        {
            if (this.Request.GetTypedHeaders().Accept.Any(
                x => x.ToString().Equals(TurboStreamMessage.MimeType, StringComparison.OrdinalIgnoreCase)))
            {
                var message = new TurboStreamMessage
                {
                    Action = TurboStreamAction.Replace,
                    Target = "stream-test",
                    TemplateInnerHtml = @"<div id=""stream-test"">Stream Test replaced " + DateTime.Now.ToString("T") + " from " + TurboStreamMessage.MimeType + " response</div>"
                };

                return this.Content(message.ToString(), TurboStreamMessage.MimeType);
            }

            this.ViewData["Status"] = $"Posted form value:\r\n{mainBody}";
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> Privacy()
        {
            var message = new TurboStreamMessage
            {
                Action = TurboStreamAction.Replace,
                Target = "stream-test",
                TemplateInnerHtml = @"<div id=""stream-test"">Stream Test replaced " + DateTime.Now.ToString("T") + " from Privacy page WebSocket send</div>"
            };

            await this.streamTestHandler.SendMessageToAllAsync(message.ToString());

            return this.View();
        }

        [HttpGet]
        public IActionResult Slow()
        {
            Thread.Sleep(2000);

            return this.View();
        }

        [HttpGet]
        public IActionResult Menu(int menuState)
        {
            switch (menuState)
            {
                case 1:
                    this.ViewData["MenuState"] = menuState.ToString();
                    break;
                default:
                    break;
            }

            return this.View();
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
