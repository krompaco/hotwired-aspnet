using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
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
            await this.SendMessageToAllAsync($"{socketId} is now connected");
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = this.WebSocketConnectionManager.GetId(socket);
            var message = $"{socketId} said: {Encoding.UTF8.GetString(buffer, 0, result.Count)}";
            await this.SendMessageToAllAsync(message);
        }
    }
}
