using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable
namespace Mliybs.QQBot.Data.OpenApi
{
    public record MessageSendResult
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
