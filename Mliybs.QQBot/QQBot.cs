using Mliybs.QQBot.Bots.Http;
using System;
using System.Threading.Tasks;

namespace Mliybs.QQBot
{
    public abstract partial class QQBot(string id, string secret) : IDisposable
    {
        internal string _id = id;
        internal string _secret = secret;

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
            AccessTokenManager.Dispose();
        }

        public AccessTokenManager AccessTokenManager { get; set; } = AccessTokenManager.GetBeforeExpire(id, secret);
    }
}
