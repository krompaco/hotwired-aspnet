using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApp.WebSocketManager
{
    public class WebSocketManagerMiddleware
    {
        private readonly RequestDelegate next;

        public WebSocketManagerMiddleware(
            RequestDelegate next,
            WebSocketHandler webSocketHandler)
        {
            this.next = next;
            this.WebSocketHandler = webSocketHandler;
        }

        private WebSocketHandler WebSocketHandler { get; }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                return;
            }

            var socket = await context.WebSockets.AcceptWebSocketAsync();
            await this.WebSocketHandler.OnConnected(socket);

            await this.Receive(socket, async (result, buffer) =>
            {
                switch (result.MessageType)
                {
                    case WebSocketMessageType.Text:
                        await this.WebSocketHandler.ReceiveAsync(socket, result, buffer);
                        return;
                    case WebSocketMessageType.Close:
                        await this.WebSocketHandler.OnDisconnected(socket);
                        return;
                }
            });

            // TODO: Investigate the Kestrel exception thrown when this is the last middleware
            ////await _next.Invoke(context);
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                try
                {
                    var result = await socket.ReceiveAsync(
                        buffer: new ArraySegment<byte>(buffer),
                        cancellationToken: CancellationToken.None);

                    handleMessage(result, buffer);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
