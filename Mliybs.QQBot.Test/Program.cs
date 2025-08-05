using Mliybs.QQBot;
using Mliybs.QQBot.Bots.WebSocket;
using Mliybs.QQBot.Data.WebSocket;

const string id = "111";

const string secret = "111";

using var bot = new WebSocketBot(id, secret);

await bot.ConnectAsync(WebSocketIntent.GroupAndC2cEvent);

bot.MessageReceived.Subscribe(async x =>
{
    await x.ReplyAsync("纯贵物啊铁子");
});

bot.KeepRunning();