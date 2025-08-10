using Mliybs.QQBot.Data;
using Mliybs.QQBot.Data.Http;
using Mliybs.QQBot.Utils;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mliybs.QQBot.Bots.Http
{
    public class HttpBot : QQBot, IDisposable
    {
        private string accessToken = default!;

        private readonly HttpListener listener = new();

        private readonly Ed25519PublicKeyParameters publicKey;
        private readonly Ed25519PrivateKeyParameters privateKey;

        public HttpBot(string id, string secret, int listenPort) : base(id, secret)
        {
            (publicKey, privateKey) = SignHelpers.GenerateKey(secret);

            listener.Prefixes.Add($"http://+:{listenPort}/");
            listener.Start();

            _ = ReceiveLoop();
        }

        public async Task<ReplyResult> SendAsync(HttpContent content)
        {
            using var req = new HttpRequestMessage(HttpMethod.Post, OpenApiHelpers.ApiUrl)
            {
                Headers = { { "Authorization", "QQBot " + accessToken } },
                Content = content
            };

            using var json = await JsonDocument.ParseAsync(await (await OpenApiHelpers.Client.SendAsync(req)).Content.ReadAsStreamAsync());

            var result = json.Deserialize<ReplyResult>(UtilHelpers.Options)!;

            if (json.RootElement.TryGetProperty("d", out var data))
                result.Data = data.Clone();

            return result;
        }

        public async Task<(ReplyResult, HttpListenerContext)> ReceiveAsync()
        {
            var context = await listener.GetContextAsync();

            using var stream = context.Request.InputStream;
            using var reader = new StreamReader(stream);

            var raw = await reader.ReadToEndAsync();

            using var json = JsonDocument.Parse(raw);

            var result = json.Deserialize<ReplyResult>(UtilHelpers.Options)!;

            if (json.RootElement.TryGetProperty("d", out var data))
                result.Data = data.Clone();

            result.Raw = raw;

            return (result, context);
        }

        private async Task ReceiveLoop()
        {
            while (true)
            {
                var (result, context) = await ReceiveAsync();

                if (result.Opcode == Opcode.CallbackUrlValidate)
                {
                    var payload = result.Data.Deserialize<CallbackValidateInfo>(UtilHelpers.Options)!;

                    var signer = SignHelpers.NewSigner(privateKey, Encoding.UTF8.GetBytes(payload.EventTimestamp));

                    var token = Encoding.UTF8.GetBytes(payload.PlainToken);

                    signer.BlockUpdate(token, 0, token.Length);

                    var sig = UtilHelpers.ToHexString(signer.GenerateSignature());

                    using var stream = context.Response.OutputStream;

                    var res = Encoding.UTF8.GetBytes(
                        JsonSerializer.Serialize(new { plain_token = payload.PlainToken, signature = sig }, UtilHelpers.Options));

                    await stream.WriteAsync(res, 0, res.Length);
                }

                context.Response.Close();
            }
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
            listener.Stop();
            listener.Close();
            ((IDisposable)listener).Dispose();
        }

        ~HttpBot()
        {
            listener.Stop();
            listener.Close();
            ((IDisposable)listener).Dispose();
        }
    }
}
