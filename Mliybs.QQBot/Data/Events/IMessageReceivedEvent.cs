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

        Task<MessageSendResult> ReplyAsync(string message);

        public interface IAuthor
        {
            public UserOpenId OpenId { get; }
        }
    }
}
