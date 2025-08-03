using Mliybs.QQBot.Data.OpenApi;
using Mliybs.QQBot.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mliybs.QQBot
{
    public abstract class AccessTokenManager(string id, string secret) : IDisposable
    {
        public static AccessTokenManager GetBeforeExpire(string id, string secret) => new GetBeforeExpireAccessTokenManager(id, secret);
        //public static AccessTokenManager GetWhenUse(string id, string secret) => new GetWhenUseAccessTokenManager();

        private string? token;

        public abstract Task StartAsync();

        protected async Task<AccessTokenInfo> RefreshToken()
        {
            var info = await OpenApiHelpers.GetAccessToken(id, secret);
            token = info.AccessToken;
            return info;
        }

        public string GetToken() => token ?? throw new NullReferenceException();

        public abstract void Dispose();
    }

    internal class GetBeforeExpireAccessTokenManager : AccessTokenManager
    {
        private readonly Timer _refreshTimer;

        private const int GetBeforeSeconds = 30;

        public GetBeforeExpireAccessTokenManager(string id, string secret) : base(id, secret)
        {
            _refreshTimer = new Timer(RefreshTokenCallback);
        }

        public override async Task StartAsync()
        {
            var info = await RefreshToken();
            UpdateRefresh(TimeSpan.FromSeconds(info.ExpiresInSeconds - GetBeforeSeconds));
        }

        private async void RefreshTokenCallback(object? state)
        {
            await RefreshToken();
        }

        public void UpdateRefresh(TimeSpan span)
        {
            _refreshTimer.Change(span, Timeout.InfiniteTimeSpan);
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
            _refreshTimer.Dispose();
        }

        ~GetBeforeExpireAccessTokenManager()
        {
            _refreshTimer.Dispose();
        }
    }

    //public class GetWhenUseAccessTokenManager : AccessTokenManager
    //{

    //}
}
