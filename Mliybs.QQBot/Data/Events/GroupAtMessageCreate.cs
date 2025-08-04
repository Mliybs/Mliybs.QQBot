using Mliybs.QQBot.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#nullable disable
namespace Mliybs.QQBot.Data.Events
{
    [Event(EventType.GroupAtMessageCreate)]
    public record GroupAtMessageCreate : IMessageReceivedEvent
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

        [JsonPropertyName("group_openid")]
        public string GroupOpenId { get; set; }

        IMessageReceivedEvent.IAuthor IMessageReceivedEvent.Author => Author;

        /// <summary>
        /// 无附件时为空数组，不为null
        /// </summary>
        [JsonPropertyName("attachments")]
        public Attachment[] Attachments { get; set; } = Array.Empty<Attachment>();

        public record AuthorType : IMessageReceivedEvent.IAuthor
        {
            [JsonPropertyName("member_openid")]
            public string MemberOpenId { get; set; }

            string IMessageReceivedEvent.IAuthor.OpenId => MemberOpenId;
        }
    }
}
