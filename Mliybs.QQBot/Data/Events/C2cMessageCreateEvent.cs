using Mliybs.QQBot.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#nullable disable
namespace Mliybs.QQBot.Data.Events
{
    [Event(EventType.C2cMessageCreate)]
    public record C2cMessageCreateEvent : IMessageReceivedEvent
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("author")]
        public AuthorType Author { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        public DateTime SendTime => Timestamp;

        IMessageReceivedEvent.IAuthor IMessageReceivedEvent.Author => Author;

        /// <summary>
        /// 无附件时为空数组，不为null
        /// </summary>
        [JsonPropertyName("attachments")]
        public Attachment[] Attachments { get; set; } = Array.Empty<Attachment>();

        public record AuthorType : IMessageReceivedEvent.IAuthor
        {
            [JsonPropertyName("user_openid")]
            public string UserOpenId { get; set; }

            string IMessageReceivedEvent.IAuthor.OpenId => UserOpenId;
        }
    }
}
