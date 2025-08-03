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
            var result = await Client.PostAsJsonAsync(AccessTokenUrl, new { appId = id, clientSecret = secret }, UtilHelpers.Options);
            return (await result.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<AccessTokenInfo>(UtilHelpers.Options))!;
        }

        public static async Task<string> GetWebSocketGateway(string accessToken)
        {
            var response = await Client.SendAsync(new(HttpMethod.Get, ApiUrl + "/gateway")
            {
                Headers = { { "Authorization", "QQBot " + accessToken } },
            });

            var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

            return json.RootElement.GetProperty("url").GetString()!;
        }
    }
}
