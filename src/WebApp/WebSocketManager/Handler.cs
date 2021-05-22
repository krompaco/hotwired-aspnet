using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApp.WebSocketManager
{
    public abstract class WebSocketHandler
    {
        protected WebSocketHandler(ConnectionManager webSocketConnectionManager, IHttpContextAccessor httpContextAccessor) => this.WebSocketConnectionManager = webSocketConnectionManager;

        protected ConnectionManager WebSocketConnectionManager { get; set; }

#pragma warning disable 1998
        public virtual async Task OnConnected(WebSocket socket)
#pragma warning restore 1998
        {
            this.WebSocketConnectionManager.AddSocket(socket);
        }

        public virtual async Task OnDisconnected(WebSocket socket)
        {
            try
            {
                await this.WebSocketConnectionManager.RemoveSocket(this.WebSocketConnectionManager.GetId(socket));
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

            await socket.SendAsync(
               buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message), offset: 0, count: message.Length),
               messageType: WebSocketMessageType.Text,
               endOfMessage: true,
               cancellationToken: CancellationToken.None);
        }

        public async Task SendMessageAsync(string socketId, string message)
        {
            await this.SendMessageAsync(this.WebSocketConnectionManager.GetSocketById(socketId), message);
        }

        public async Task SendMessageToAllAsync(string message)
        {
            foreach (var pair in this.WebSocketConnectionManager.GetAll())
            {
                if (pair.Value.State == WebSocketState.Open)
                {
                    await this.SendMessageAsync(pair.Value, message);
                }
            }
        }

        // TODO: Decide if exposing the message string is better than exposing the result and buffer
        public abstract Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}
