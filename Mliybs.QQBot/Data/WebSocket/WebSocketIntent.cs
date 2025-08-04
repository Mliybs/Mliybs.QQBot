using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mliybs.QQBot.Data.WebSocket
{
    [Flags]
    public enum WebSocketIntent
    {
        /// <summary>
        /// <para>绑定所有事件（包括群与频道），如果有事件没有权限则会断开连接，谨慎使用</para>
        /// <para>不关心频道建议使用GroupAndC2cEvent</para>
        /// </summary>
        All = int.MaxValue,

        Guilds = 1 << 0,

        GuildMembers = 1 << 1,

        GuildMessages = 1 << 9,

        GuildMessageReactions = 1 << 10,

        /// <summary>
        /// 频道私信
        /// </summary>
        DirectMessage = 1 << 12,

        GroupAndC2cEvent = 1 << 25,

        Interaction = 1 << 26,

        MessageAudit = 1 << 27,

        ForumsEvent = 1 << 28,

        AudioAction = 1 << 29,

        PublicGuildMessages = 1 << 30
    }
}
