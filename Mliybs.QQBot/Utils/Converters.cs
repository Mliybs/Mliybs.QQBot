using Mliybs.QQBot.Data;
using Mliybs.QQBot.Data.Events;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mliybs.QQBot.Utils
{
    public class AttachmentConverter : JsonConverter<ContentType>
    {
        public override ContentType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String) return ContentType.Unknown;

            return reader.GetString() switch
            {
                "image/jpeg" => ContentType.Jpeg,
                "image/png" => ContentType.Png,
                "image/gif" => ContentType.Gif,
                "file" => ContentType.File,
                "video/mp4" => ContentType.Mp4,
                "voice" => ContentType.Voice,
                _ => ContentType.Unknown,
            };
        }

        public override void Write(Utf8JsonWriter writer, ContentType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value switch
            {
                ContentType.Jpeg => "image/jpeg",
                ContentType.Png => "image/png",
                ContentType.Gif => "image/gif",
                ContentType.File => "file",
                ContentType.Mp4 => "video/mp4",
                ContentType.Voice => "voice",
                _ => throw new NotSupportedException()
            });
        }
    }

    public class UserOpenIdConverter : JsonConverter<UserOpenId>
    {
        public override UserOpenId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String) throw new JsonException();

            return new(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, UserOpenId value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.OpenId);
        }
    }

    public class GroupOpenIdConverter : JsonConverter<GroupOpenId>
    {
        public override GroupOpenId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String) throw new JsonException();

            return new(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, GroupOpenId value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.OpenId);
        }
    }
}
