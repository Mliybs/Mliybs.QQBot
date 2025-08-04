using Mliybs.QQBot.Buffers;
using Mliybs.QQBot.Data;
using Mliybs.QQBot.Data.Events;
using Mliybs.QQBot.Data.Http;
using Mliybs.QQBot.Data.WebSocket;
using Mliybs.QQBot.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Mliybs.QQBot.Bots.WebSocket
{
    public class WebSocketBot : QQBot, IDisposable
    {
        private readonly ClientWebSocket websocket = new();

        private readonly Timer heartbeatTimer;

        private int? currentSerialNumber = null;

        public WebSocketBot(string id, string secret) : base(id, secret)
        {
            heartbeatTimer = new(SendHeartbeat);
        }

        public async Task<ReadyEvent> ConnectAsync(WebSocketIntent subscribeIntents = WebSocketIntent.GroupAndC2cEvent)
        {
            await AccessTokenManager.StartAsync(); // 刷新AccessToken



            await websocket.ConnectAsync(new(await OpenApiHelpers.GetWebSocketGateway(AccessTokenManager.GetToken())), CancellationToken.None);

            var interval = TimeSpan.FromMilliseconds((await ReceiveAsync()).Data.GetProperty("heartbeat_interval").GetInt32());

            heartbeatTimer.Change(interval, interval);

            await websocket.SendAsync(Encoding.UTF8.GetBytes($$"""
                {
                  "op": 2,
                  "d": {
                    "token": "QQBot {{AccessTokenManager.GetToken()}}",
                    "intents": {{(int)subscribeIntents}}
                  }
                }
                """), WebSocketMessageType.Text, true, CancellationToken.None);

            var ready = await ReceiveAsync();

            if (ready.Opcode == Opcode.InvalidSession) throw new Exception();

            _ = ReceiveLoop();

            return ready.Data.Deserialize<ReadyEvent>(UtilHelpers.Options)!;
        }

        public async Task<ReplyResult> ReceiveAsync()
        {
            using var reader = new WebSocketPipeReader();

            var bytes = await reader.ReadToEndAsync(websocket);

            var raw = Encoding.UTF8.GetString(bytes);

            var json = JsonDocument.Parse(bytes);

            var result = json.Deserialize<ReplyResult>(UtilHelpers.Options)!;

            json.RootElement.TryGetProperty("d", out result.Data);

            result.Raw = raw;

            if (result.SerialNumber.HasValue) currentSerialNumber = result.SerialNumber.Value;

            return result;
        }

        private async Task ReceiveLoop()
        {
            while (true)
            {
                var result = await ReceiveAsync();

                if (result.Opcode == Opcode.Dispatch)
                {
                    if (UtilHelpers.EventTypes.TryGetValue(result.Type, out var type))
                    {
                        messageReceived.OnNext((IMessageReceivedEvent)result.Data.Deserialize(type, UtilHelpers.Options)!);
                        return;
                    }
                }

                eventReceived.OnNext(result);
            }
        }

        private void SendHeartbeat(object? _)
        {
            Console.WriteLine(currentSerialNumber.HasValue ? currentSerialNumber.Value : "null");
            _ = websocket.SendAsync(Encoding.UTF8.GetBytes($$"""
                {
                  "op": 1,
                  "d": {{(currentSerialNumber.HasValue ? currentSerialNumber.Value : "null")}}
                }
                """), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
            websocket.Dispose();
        }

        ~WebSocketBot()
        {
            websocket.Dispose();
        }
    }
}
