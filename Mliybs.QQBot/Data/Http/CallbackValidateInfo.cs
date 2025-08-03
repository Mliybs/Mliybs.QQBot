using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable
namespace Mliybs.QQBot.Data.Http
{
    public record CallbackValidateInfo
    {
        [JsonPropertyName("plain_token")]
        public string PlainToken { get; set; }

        [JsonPropertyName("event_ts")]
        public string EventTimestamp { get; set; }
    }
}
