using Mliybs.QQBot.Data;
using Mliybs.QQBot.Data.Exceptions;
using Mliybs.QQBot.Data.OpenApi;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mliybs.QQBot.Utils
{
    public static class OpenApiHelpers
    {
        public const string AccessTokenUrl = "https://bots.qq.com/app/getAppAccessToken";
        public const string ApiUrl = "https://api.sgroup.qq.com";

        public static readonly HttpClient Client = new();

        public static async Task<AccessTokenInfo> GetAccessToken(string id, string secret)
        {
            using var content = JsonContent.Create(new { appId = id, clientSecret = secret }, options: UtilHelpers.Options);

            using var result = await Client.PostAsync(AccessTokenUrl, content);

            using var json = JsonDocument.Parse(await result.EnsureSuccessStatusCode()
                .Content.ReadAsStringAsync());

            return result.IsSuccessStatusCode
                ? json.Deserialize<AccessTokenInfo>(UtilHelpers.Options)!
                : throw new OpenApiException(json.Deserialize<OpenApiExceptionContext>(UtilHelpers.Options)!);
        }

        public static async Task<string> GetWebSocketGateway(string accessToken)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, ApiUrl + "/gateway")
            {
                Headers = { { "Authorization", "QQBot " + accessToken } },
            };

            using var res = await Client.SendAsync(req);

            using var json = JsonDocument.Parse(await res.Content.ReadAsStringAsync());

            return res.IsSuccessStatusCode
                ? json.RootElement.GetProperty("url").GetString()!
                : throw new OpenApiException(json.Deserialize<OpenApiExceptionContext>(UtilHelpers.Options)!);
        }

        public static async Task<MessageSendResult> SendC2cMessage(UserOpenId openId, string accessToken, object message)
        {
            using var content = JsonContent.Create(message, options: UtilHelpers.Options);
            using var req = new HttpRequestMessage(HttpMethod.Post, ApiUrl + $"/v2/users/{openId.OpenId}/messages")
            {
                Headers = { { "Authorization", "QQBot " + accessToken } },
                Content = content
            };

            using var result = await Client.SendAsync(req);

            using var json = JsonDocument.Parse(await result.Content.ReadAsStringAsync());

            return result.IsSuccessStatusCode
                ? json.Deserialize<MessageSendResult>(UtilHelpers.Options)!
                : throw new OpenApiException(json.Deserialize<OpenApiExceptionContext>(UtilHelpers.Options)!);
        }

        public static async Task<MessageSendResult> SendGroupMessage(GroupOpenId openId, string accessToken, object message)
        {
            using var content = JsonContent.Create(message, options: UtilHelpers.Options);
            using var req = new HttpRequestMessage(HttpMethod.Post, ApiUrl + $"/v2/groups/{openId.OpenId}/messages")
            {
                Headers = { { "Authorization", "QQBot " + accessToken } },
                Content = content
            };

            using var result = await Client.SendAsync(req);

            using var json = JsonDocument.Parse(await result.Content.ReadAsStringAsync());

            return result.IsSuccessStatusCode
                ? json.Deserialize<MessageSendResult>(UtilHelpers.Options)!
                : throw new OpenApiException(json.Deserialize<OpenApiExceptionContext>(UtilHelpers.Options)!);
        }

        public static async Task<FileInfoResult> RequestFileInfo(UserOpenId userOpenId, string accessToken, FileType type, string urlOrBase64, bool isBase64)
        {
            using var content = isBase64 ? JsonContent.Create(new
            {
                file_type = (int)type,
                srv_send_msg = false,
                file_data = urlOrBase64
            }, options: UtilHelpers.Options) : JsonContent.Create(new
            {
                file_type = (int)type,
                srv_send_msg = false,
                url = urlOrBase64
            }, options: UtilHelpers.Options);

            using var req = new HttpRequestMessage(HttpMethod.Post, ApiUrl + $"/v2/users/{userOpenId.OpenId}/files")
            {
                Headers = { { "Authorization", "QQBot " + accessToken } },
                Content = content
            };

            using var result = await Client.SendAsync(req);

            using var json = JsonDocument.Parse(await result.Content.ReadAsStringAsync());

            return result.IsSuccessStatusCode
                ? json.Deserialize<FileInfoResult>(UtilHelpers.Options)!
                : throw new OpenApiException(json.Deserialize<OpenApiExceptionContext>(UtilHelpers.Options)!);
        }

        public static async Task<FileInfoResult> RequestFileInfo(UserOpenId userOpenId, string accessToken, FileType type, ReadOnlyMemory<byte> data)
        {
            using var content = JsonContent.Create(new
            {
                file_type = (int)type,
                srv_send_msg = false,
                file_data = Convert.ToBase64String(data.Span)
            }, options: UtilHelpers.Options);

            using var req = new HttpRequestMessage(HttpMethod.Post, ApiUrl + $"/v2/users/{userOpenId.OpenId}/files")
            {
                Headers = { { "Authorization", "QQBot " + accessToken } },
                Content = content
            };

            using var result = await Client.SendAsync(req);

            using var json = JsonDocument.Parse(await result.Content.ReadAsStringAsync());

            return result.IsSuccessStatusCode
                ? json.Deserialize<FileInfoResult>(UtilHelpers.Options)!
                : throw new OpenApiException(json.Deserialize<OpenApiExceptionContext>(UtilHelpers.Options)!);
        }

        public static async Task<FileInfoResult> RequestFileInfo(GroupOpenId groupOpenId, string accessToken, FileType type, string urlOrBase64, bool isBase64)
        {
            using var content = isBase64 ? JsonContent.Create(new
            {
                file_type = (int)type,
                srv_send_msg = false,
                file_data = urlOrBase64
            }, options: UtilHelpers.Options) : JsonContent.Create(new
            {
                file_type = (int)type,
                srv_send_msg = false,
                url = urlOrBase64
            }, options: UtilHelpers.Options);

            using var req = new HttpRequestMessage(HttpMethod.Post, ApiUrl + $"/v2/groups/{groupOpenId.OpenId}/files")
            {
                Headers = { { "Authorization", "QQBot " + accessToken } },
                Content = content
            };

            using var result = await Client.SendAsync(req);

            using var json = JsonDocument.Parse(await result.Content.ReadAsStringAsync());

            return result.IsSuccessStatusCode
                ? json.Deserialize<FileInfoResult>(UtilHelpers.Options)!
                : throw new OpenApiException(json.Deserialize<OpenApiExceptionContext>(UtilHelpers.Options)!);
        }

        public static async Task<FileInfoResult> RequestFileInfo(GroupOpenId groupOpenId, string accessToken, FileType type, ReadOnlyMemory<byte> data)
        {
            using var content = JsonContent.Create(new
            {
                file_type = (int)type,
                srv_send_msg = false,
                file_data = Convert.ToBase64String(data.Span)
            }, options: UtilHelpers.Options);

            using var req = new HttpRequestMessage(HttpMethod.Post, ApiUrl + $"/v2/groups/{groupOpenId.OpenId}/files")
            {
                Headers = { { "Authorization", "QQBot " + accessToken } },
                Content = content
            };

            using var result = await Client.SendAsync(req);

            using var json = JsonDocument.Parse(await result.Content.ReadAsStringAsync())!;

            return result.IsSuccessStatusCode
                ? json.Deserialize<FileInfoResult>(UtilHelpers.Options)!
                : throw new OpenApiException(json.Deserialize<OpenApiExceptionContext>(UtilHelpers.Options)!);
        }
    }
}
