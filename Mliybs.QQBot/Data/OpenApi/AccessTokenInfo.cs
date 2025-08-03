using System;
using System.Text.Json.Serialization;

#nullable disable
namespace Mliybs.QQBot.Data.OpenApi
{
    public record AccessTokenInfo
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresInSeconds { get; set; }
    }
}
