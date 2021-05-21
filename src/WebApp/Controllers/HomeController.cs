using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        private readonly StreamTestHandler streamTestHandler;

        public HomeController(ILogger<HomeController> logger, StreamTestHandler streamTestHandler)
        {
            this.logger = logger;
            this.streamTestHandler = streamTestHandler;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string mainBody)
        {
            ViewData["Status"] = $"Posted form value:\r\n{mainBody}";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Privacy()
        {
            await streamTestHandler.SendMessageToAllAsync(@"<turbo-stream action=""replace"" target=""stream-test"">
    <template>
        <div id=""stream-test"">
            Stream Test replaced from Privacy page WebSocket send
        </div>
    </template>
</turbo-stream>");

            return View();
        }

        [HttpGet]
        public IActionResult Slow()
        {
            Thread.Sleep(2000);

            return View();
        }

        [HttpGet]
        public IActionResult Menu(int menuState)
        {
            switch (menuState)
            {
                case 1:
                    ViewData["MenuState"] = menuState.ToString();
                    break;
                default:
                    break;
            }

            return View();
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
