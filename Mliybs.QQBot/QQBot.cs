using Mliybs.QQBot.Network;
using Mliybs.QQBot.Network.Http;
using System;

namespace Mliybs.QQBot
{
    partial class QQBot
    {
        public static QQBot Http(string id, string secret, int listenPort) => new(new HttpHandler(id, secret, listenPort));
    }

    public partial class QQBot
    {
        private readonly IQQBotHandler handler;

        public QQBot(IQQBotHandler handler)
        {
            this.handler = handler;
        }
    }
}
