using System;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Krompaco.HotwiredAspNet.TurboStreams;
using Microsoft.AspNetCore.Http;

namespace WebApp.WebSocketManager
{
    public abstract class WebSocketHandler
    {
        protected WebSocketHandler(ConnectionManager webSocketConnectionManager, IHttpContextAccessor httpContextAccessor) => this.WebSocketConnectionManager = webSocketConnectionManager;

        protected ConnectionManager WebSocketConnectionManager { get; set; }

        public virtual async Task OnConnected(WebSocket socket)
        {
            this.WebSocketConnectionManager.AddSocket(socket);

            var message = new TurboStreamMessage
            {
                Action = TurboStreamAction.Update,
                Target = "stream-test",
                TemplateInnerHtml = "Socket connected",
            };

            await this.SendMessageAsync(socket, message.ToString()).ConfigureAwait(false);
        }

        public virtual async Task OnDisconnected(WebSocket socket)
        {
            try
            {
                await this.WebSocketConnectionManager.RemoveSocket(this.WebSocketConnectionManager.GetId(socket)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task SendMessageAsync(WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
            {
                return;
            }

            var encodedMessage = Encoding.UTF8.GetBytes(message);
            await socket.SendAsync(
                buffer: new ArraySegment<byte>(array: encodedMessage, offset: 0, count: encodedMessage.Length),
                messageType: WebSocketMessageType.Text,
                endOfMessage: true,
                cancellationToken: CancellationToken.None).ConfigureAwait(false);
        }

        public async Task SendMessageAsync(string socketId, string message)
        {
            await this.SendMessageAsync(this.WebSocketConnectionManager.GetSocketById(socketId), message).ConfigureAwait(false);
        }

        public async Task SendMessageToAllAsync(string message)
        {
            foreach (var pair in this.WebSocketConnectionManager.GetAll())
            {
                if (pair.Value.State == WebSocketState.Open)
                {
                    await this.SendMessageAsync(pair.Value, message).ConfigureAwait(false);
                }
            }
        }

        // TODO: Decide if exposing the message string is better than exposing the result and buffer
        public abstract Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}
