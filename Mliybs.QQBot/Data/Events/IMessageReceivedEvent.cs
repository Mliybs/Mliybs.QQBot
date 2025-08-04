using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mliybs.QQBot.Data.Events
{
    public interface IMessageReceivedEvent
    {
        public string Id { get; }

        public string Content { get; }

        public IAuthor Author { get; }

        public DateTime SendTime { get; }

        public Attachment[] Attachments { get; }

        public interface IAuthor
        {
            public string OpenId { get; }
        }
    }
}
