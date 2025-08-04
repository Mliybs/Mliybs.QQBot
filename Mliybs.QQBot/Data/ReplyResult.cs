using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mliybs.QQBot.Data
{
#nullable disable
    public record ReplyResult
    {
        private JsonElement data;

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("op")]
        public Opcode Opcode { get; set; }

        [JsonPropertyName("s")]
        public int? SerialNumber { get; set; }

        [JsonPropertyName("t")]
        public EventType Type { get; set; }

        [JsonPropertyName("d"), JsonIgnore]
        public ref JsonElement Data => ref data;

        [JsonIgnore]
        public string Raw {  get; set; }
    }
}
