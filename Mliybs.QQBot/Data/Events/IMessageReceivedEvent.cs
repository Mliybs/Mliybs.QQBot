using Mliybs.QQBot.Data.OpenApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mliybs.QQBot.Data.Events
{
    public interface IMessageReceivedEvent
    {
        string Id { get; }

        string Content { get; }

        IAuthor Author { get; }

        DateTime SendTime { get; }

        Attachment[] Attachments { get; }

        QQBot Bot { get; internal set; }

        Task<MessageSendResult> ReplyAsync(string message, FileInfoResult? file = null);

        Task<MessageSendResult> ReplyMarkdownAsync(string markdown);

        Task<FileInfoResult> RequestFileInfo(FileType type, string urlOrBase64, bool isBase64);

        Task<FileInfoResult> RequestFileInfo(FileType type, byte[] data);

        Task<FileInfoResult> RequestFileInfo(FileType type, ReadOnlyMemory<byte> data);

        public interface IAuthor
        {
            public UserOpenId OpenId { get; }
        }
    }
}
