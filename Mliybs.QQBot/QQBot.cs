using Mliybs.QQBot.Bots.Http;
using Mliybs.QQBot.Data;
using Mliybs.QQBot.Data.Events;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace Mliybs.QQBot
{
    public abstract partial class QQBot(string id, string secret) : IDisposable
    {
        internal string _id = id;
        internal string _secret = secret;

        private readonly AutoResetEvent keepRunning = new(false);

        protected readonly Subject<ReplyResult> eventReceived = new();

        protected readonly Subject<IMessageReceivedEvent> messageReceived = new();

        public void KeepRunning()
        {
            keepRunning.WaitOne();
        }

        public void StopRunning()
        {
            keepRunning.Set();
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
            keepRunning.Dispose();
            AccessTokenManager.Dispose();
        }

        ~QQBot()
        {
            keepRunning.Dispose();
            AccessTokenManager.Dispose();
        }

        public AccessTokenManager AccessTokenManager { get; set; } = AccessTokenManager.GetBeforeExpire(id, secret);

        public IObservable<ReplyResult> EventReceived => eventReceived.AsObservable().ObserveOn(Scheduler.Default);

        public IObservable<IMessageReceivedEvent> MessageReceived => messageReceived.AsObservable().ObserveOn(Scheduler.Default);
    }
}
