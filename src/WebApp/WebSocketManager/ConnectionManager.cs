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
        private readonly ConcurrentDictionary<string, WebSocket> _sockets = new();

        private readonly IHttpContextAccessor httpContextAccessor;

        public ConnectionManager(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public WebSocket GetSocketById(string id)
        {
            return _sockets.FirstOrDefault(p => p.Key == id).Value;
        }

        public ConcurrentDictionary<string, WebSocket> GetAll()
        {
            return _sockets;
        }

        public string GetId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value == socket).Key;
        }

        public void AddSocket(WebSocket socket)
        {
            _sockets.TryAdd(this.httpContextAccessor.HttpContext.Session.GetOrCreateSessionId() ?? "no-session-" + Guid.NewGuid().ToString("D"), socket);
        }

        public async Task RemoveSocket(string id)
        {
            WebSocket socket;
            _sockets.TryRemove(id, out socket);

            await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure, 
                                    statusDescription: "Closed by the ConnectionManager", 
                                    cancellationToken: CancellationToken.None);
        }
    }
}
