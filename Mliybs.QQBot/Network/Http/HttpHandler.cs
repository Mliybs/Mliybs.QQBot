using Mliybs.QQBot.Buffers;
using Mliybs.QQBot.Data;
using Mliybs.QQBot.Utils;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mliybs.QQBot.Network.Http
{
    public partial class HttpHandler : IQQBotHandler
    {
        private readonly string id;
        private readonly string secret;

        private string accessToken = default!;

        private readonly HttpClient client = new();

        private readonly HttpListener listener = new();

        private readonly Ed25519PublicKeyParameters publicKey;
        private readonly Ed25519PrivateKeyParameters privateKey;

        public HttpHandler(string id, string secret, int listenPort)
        {
            this.id = id;
            this.secret = secret;

            (publicKey, privateKey) = SignHelpers.GenerateKey(secret);

            listener.Prefixes.Add($"http://+:{listenPort}/");
            listener.Start();
        }

        public async Task<ReplyResult> SendAsync(HttpContent content)
        {
            var json = await JsonDocument.ParseAsync(await (await client.SendAsync(new(HttpMethod.Post, ApiUrl)
            {
                Headers = { { "Authorization", "QQBot " + accessToken } },
                Content = content
            })).Content.ReadAsStreamAsync());

            var result = json.Deserialize<ReplyResult>(UtilHelpers.Options)!;

            json.RootElement.TryGetProperty("d", out result.Data);

            return result;
        }

        public async Task<ReplyResult> ReceiveAsync()
        {
            var context = await listener.GetContextAsync();

            var signature = Convert.FromHexString(context.Request.Headers["X-Signature-Ed25519"]!);
            var timestamp = Encoding.UTF8.GetBytes(context.Request.Headers["X-Signature-Timestamp"]!);

            using var stream = context.Request.InputStream;
            using var reader = new StreamPipeReader();

            var signer = SignHelpers.NewSigner(publicKey, timestamp);

            var json = JsonDocument.Parse(await reader.ReadToEndAsync(stream, signer.BlockUpdate));

            var result = json.Deserialize<ReplyResult>(UtilHelpers.Options)!;

            json.RootElement.TryGetProperty("d", out result.Data);

            return result;
        }

        public record AccessTokenInfo(string AccessToken, int ExpiresIn);

        public async Task<AccessTokenInfo> GetAccessToken()
        {
            var result = await client.PostAsJsonAsync(AccessTokenUrl, new { appId = id, clientSecret = secret }, UtilHelpers.Options);
            return (await result.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<AccessTokenInfo>(UtilHelpers.Options))!;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            listener.Stop();
            listener.Close();
            ((IDisposable)listener).Dispose();
            client?.Dispose();
        }

        ~HttpHandler()
        {
            listener.Stop();
            listener.Close();
            ((IDisposable)listener).Dispose();
            client?.Dispose();
        }
    }

    partial class HttpHandler
    {
        public const string AccessTokenUrl = "https://bots.qq.com/app/getAppAccessToken";
        public const string ApiUrl = "https://api.sgroup.qq.com";
    }
}
