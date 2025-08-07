using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#nullable disable
namespace Mliybs.QQBot.Data.OpenApi
{
    public record FileInfoResult
    {
        [JsonPropertyName("file_uuid")]
        public string FileUuid { get; set; }

        [JsonPropertyName("file_info")]
        public string FileInfo { get; set; }

        [JsonPropertyName("ttl")]
        public int TimeToLiveSeconds { get; set; }
#nullable enable
        [JsonPropertyName("id"), Obsolete("srv_send_msg为true时返回，但QQ目前已不提供主动推送服务")]
        public string? Id { get; set; }
    }
}
