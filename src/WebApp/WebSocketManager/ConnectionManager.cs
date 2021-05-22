using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApp.Extensions;

namespace WebApp.WebSocketManager
{
    public class ConnectionManager
    {
        private readonly ConcurrentDictionary<string, WebSocket> sockets = new ();

        private readonly IHttpContextAccessor httpContextAccessor;

        public ConnectionManager(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public WebSocket GetSocketById(string id)
        {
            return this.sockets.FirstOrDefault(p => p.Key == id).Value;
        }

        public ConcurrentDictionary<string, WebSocket> GetAll()
        {
            return this.sockets;
        }

        public string GetId(WebSocket socket)
        {
            return this.sockets.FirstOrDefault(p => p.Value == socket).Key;
        }

        public void AddSocket(WebSocket socket)
        {
            this.sockets.TryAdd(this.httpContextAccessor.HttpContext?.Session.GetOrCreateSessionId() ?? "no-session-" + Guid.NewGuid().ToString("D"), socket);
        }

        public async Task RemoveSocket(string id)
        {
            this.sockets.TryRemove(id, out var socket);

            if (socket == null)
            {
                return;
            }

            await socket.CloseAsync(
                closeStatus: WebSocketCloseStatus.NormalClosure,
                statusDescription: "Closed by the ConnectionManager",
                cancellationToken: CancellationToken.None);
        }
    }
}
