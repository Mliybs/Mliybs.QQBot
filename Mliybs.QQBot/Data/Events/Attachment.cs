using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#nullable disable
namespace Mliybs.QQBot.Data.Events
{
    // 其实这里的nullable性我还不确定
    public record class Attachment
    {
        [JsonPropertyName("content_type")]
        public ContentType ContentType { get; set; }

        [JsonPropertyName("filename")]
        public string FileName { get; set; }

        [JsonPropertyName("height")]
        public int? Height { get; set; }

        [JsonPropertyName("width")]
        public int? Width { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public enum ContentType
    {
        Unknown,
        Jpeg,
        Png,
        Gif,
        File,
        Mp4,
        Voice
    }
}
