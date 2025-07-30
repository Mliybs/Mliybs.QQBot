using Mliybs.QQBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mliybs.QQBot.Network.WebSocket
{
    public class WebSocketHandler : IQQBotHandler
    {
        private readonly ClientWebSocket websocket = new();

        public async Task ConnectAsync(string url)
        {
            await websocket.ConnectAsync(new(url), CancellationToken.None);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            websocket.Dispose();
        }

        ~WebSocketHandler()
        {
            websocket.Dispose();
        }
    }
}
