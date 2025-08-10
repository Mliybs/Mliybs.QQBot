using Mliybs.QQBot.Data.Attributes;
using Mliybs.QQBot.Data.OpenApi;
using Mliybs.QQBot.Utils;
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

        [JsonIgnore]
        public QQBot Bot { get; internal set; }

        [JsonIgnore]
        QQBot IMessageReceivedEvent.Bot { get => Bot; set => Bot = value; }

        public record AuthorType : IMessageReceivedEvent.IAuthor
        {
            [JsonPropertyName("user_openid")]
            public UserOpenId UserOpenId { get; set; }

            UserOpenId IMessageReceivedEvent.IAuthor.OpenId => UserOpenId;
        }

        private int currentSerialNumber = 1;

#nullable enable
        public async Task<MessageSendResult> ReplyAsync(string message, FileInfoResult? file = null)
        {
            return await OpenApiHelpers.SendC2cMessage(Author.UserOpenId, Bot.AccessTokenManager.GetToken(), new
            {
                msg_type = (int)(file is null ? MessageType.Text : MessageType.Media),
                content = message,
                msg_id = Id,
                media = file,
                msg_seq = currentSerialNumber++
            });
        }
#nullable disable
        public async Task<MessageSendResult> ReplyMarkdownAsync(string markdown)
        {
            return await OpenApiHelpers.SendC2cMessage(Author.UserOpenId, Bot.AccessTokenManager.GetToken(), new
            {
                msg_type = (int)MessageType.Markdown,
                msg_id = Id,
                markdown = new
                {
                    content = markdown
                },
                msg_seq = currentSerialNumber++
            });
        }

        public async Task<FileInfoResult> RequestFileInfo(FileType type, string urlOrBase64, bool isBase64)
        {
            return await OpenApiHelpers.RequestFileInfo(Author.UserOpenId, Bot.AccessTokenManager.GetToken(), type, urlOrBase64, isBase64);
        }

        public async Task<FileInfoResult> RequestFileInfo(FileType type, byte[] data)
        {
            return await OpenApiHelpers.RequestFileInfo(Author.UserOpenId, Bot.AccessTokenManager.GetToken(), type, data);
        }

        public async Task<FileInfoResult> RequestFileInfo(FileType type, ReadOnlyMemory<byte> data)
        {
            return await OpenApiHelpers.RequestFileInfo(Author.UserOpenId, Bot.AccessTokenManager.GetToken(), type, data);
        }
    }
}
