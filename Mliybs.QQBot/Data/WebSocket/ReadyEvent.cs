using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#nullable disable
namespace Mliybs.QQBot.Data.WebSocket
{
    public record ReadyEvent
    {
        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }

        [JsonPropertyName("user")]
        public UserInfo User { get; set; }

        [JsonPropertyName("shard")]
        public int[] Shard { get; set; } = Array.Empty<int>();


        public record UserInfo
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("username")]
            public string UserName { get; set; }

            [JsonPropertyName("bot")]
            public bool IsBot { get; set; }
        }
    }
}
