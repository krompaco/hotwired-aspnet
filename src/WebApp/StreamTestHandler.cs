using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Krompaco.HotwiredAspNet.TurboStreams;
using Microsoft.AspNetCore.Http;
using WebApp.WebSocketManager;

namespace WebApp
{
    public class StreamTestHandler : WebSocketHandler
    {
        public StreamTestHandler(ConnectionManager webSocketConnectionManager, IHttpContextAccessor httpContextAccessor)
            : base(webSocketConnectionManager, httpContextAccessor)
        {
        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);

            var socketId = this.WebSocketConnectionManager.GetId(socket);

            var message = new TurboStreamMessage
            {
                Action = TurboStreamAction.Update,
                Target = "stream-test",
                TemplateInnerHtml = $"{socketId} is now connected",
            };

            await this.SendMessageToAllAsync(message.ToString());
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = this.WebSocketConnectionManager.GetId(socket);

            var message = new TurboStreamMessage
            {
                Action = TurboStreamAction.Update,
                Target = "stream-test",
                TemplateInnerHtml = $"{socketId} said: {Encoding.UTF8.GetString(buffer, 0, result.Count)}",
            };

            await this.SendMessageToAllAsync(message.ToString());
        }
    }
}
