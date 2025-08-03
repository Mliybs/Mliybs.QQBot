using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mliybs.QQBot.Data
{
    public enum EventType
    {
        None = 0,

        Ready = 1,

        Resumed = 2,

        #region Guilds

        /// <summary>
        /// 当机器人加入新频道时
        /// </summary>
        GuildCreate = 100,

        /// <summary>
        /// 当频道资料发生变更时
        /// </summary>
        GuildUpdate,

        /// <summary>
        /// 当机器人退出频道时
        /// </summary>
        GuildDelete,

        /// <summary>
        /// 当子频道被创建时
        /// </summary>
        ChannelCreate,

        /// <summary>
        /// 当子频道被更新时
        /// </summary>
        ChannelUpdate,

        /// <summary>
        /// 当子频道被删除时
        /// </summary>
        ChannelDelete,

        #endregion
        #region GuildMembers

        /// <summary>
        /// 当成员加入时
        /// </summary>
        GuildMemberAdd = 200,

        /// <summary>
        /// 当成员资料变更时
        /// </summary>
        GuildMemberUpdate,

        /// <summary>
        /// 当成员被移除时
        /// </summary>
        GuildMemberRemove,

        #endregion
        #region GuildMessages

        /// <summary>
        /// 发送消息事件，代表频道内的全部消息
        /// </summary>
        MessageCreate = 300,

        /// <summary>
        /// 删除（撤回）消息事件
        /// </summary>
        MessageDelete,

        #endregion
        #region GuildMessageReactions

        /// <summary>
        /// 为消息添加表情表态
        /// </summary>
        MessageReactionAdd = 400,

        /// <summary>
        /// 为消息删除表情表态
        /// </summary>
        MessageReactionRemove,

        #endregion
        #region DirectMessage

        /// <summary>
        /// 当收到用户发给机器人的私信消息时
        /// </summary>
        DirectMessageCreate = 500,

        /// <summary>
        /// 删除（撤回）私信消息事件
        /// </summary>
        DirectMessageDelete,

        #endregion
        #region GroupAndC2cEvent

        /// <summary>
        /// 用户单聊发消息给机器人时候
        /// </summary>
        C2cMessageCreate = 600,

        /// <summary>
        /// 用户添加使用机器人
        /// </summary>
        FriendAdd,

        /// <summary>
        /// 用户删除机器人
        /// </summary>
        FriendDel,

        /// <summary>
        /// 用户在机器人资料卡手动关闭"主动消息"推送
        /// </summary>
        C2cMsgReject,

        /// <summary>
        /// 用户在机器人资料卡手动开启"主动消息"推送开关
        /// </summary>
        C2cMsgReceive,

        /// <summary>
        /// 用户在群里@机器人时收到的消息
        /// </summary>
        GroupAtMessageCreate,

        /// <summary>
        /// 机器人被添加到群聊
        /// </summary>
        GroupAddRobot,

        /// <summary>
        /// 机器人被移出群聊
        /// </summary>
        GroupDelRobot,

        /// <summary>
        /// 群管理员主动在机器人资料页操作关闭通知
        /// </summary>
        GroupMsgReject,

        /// <summary>
        /// 群管理员主动在机器人资料页操作开启通知
        /// </summary>
        GroupMsgReceive,

        #endregion
        #region Interaction

        /// <summary>
        /// 互动事件创建时
        /// </summary>
        InteractionCreate = 700,

        #endregion
        #region MessageAudit

        /// <summary>
        /// 消息审核通过
        /// </summary>
        MessageAuditPass = 800,

        /// <summary>
        /// 消息审核不通过
        /// </summary>
        MessageAuditReject,

        #endregion
        #region ForumsEvent

        /// <summary>
        /// 当用户创建主题时
        /// </summary>
        ForumThreadCreate = 900,

        /// <summary>
        /// 当用户更新主题时
        /// </summary>
        ForumThreadUpdate,

        /// <summary>
        /// 当用户删除主题时
        /// </summary>
        ForumThreadDelete,

        /// <summary>
        /// 当用户创建帖子时
        /// </summary>
        ForumPostCreate,

        /// <summary>
        /// 当用户删除帖子时
        /// </summary>
        ForumPostDelete,

        /// <summary>
        /// 当用户回复评论时
        /// </summary>
        ForumReplyCreate,

        /// <summary>
        /// 当用户删除回复时
        /// </summary>
        ForumReplyDelete,

        /// <summary>
        /// 当用户发表审核通过时
        /// </summary>
        ForumPublishAuditResult,

        #endregion
        #region AudioAction

        /// <summary>
        /// 音频开始播放时
        /// </summary>
        AudioStart = 1000,

        /// <summary>
        /// 音频播放结束时
        /// </summary>
        AudioFinish,

        /// <summary>
        /// 上麦时
        /// </summary>
        AudioOnMic,

        /// <summary>
        /// 下麦时
        /// </summary>
        AudioOffMic,

        #endregion
        #region PublicGuildMessages

        /// <summary>
        /// 当收到@机器人的消息时
        /// </summary>
        AtMessageCreate = 1100,

        /// <summary>
        /// 当频道的消息被删除时
        /// </summary>
        PublicMessageDelete

        #endregion
    }

    public enum GuildsType
    {

        #region Guilds

        /// <summary>
        /// 当机器人加入新频道时
        /// </summary>
        GuildCreate = 100,

        /// <summary>
        /// 当频道资料发生变更时
        /// </summary>
        GuildUpdate,

        /// <summary>
        /// 当机器人退出频道时
        /// </summary>
        GuildDelete,

        /// <summary>
        /// 当子频道被创建时
        /// </summary>
        ChannelCreate,

        /// <summary>
        /// 当子频道被更新时
        /// </summary>
        ChannelUpdate,

        /// <summary>
        /// 当子频道被删除时
        /// </summary>
        ChannelDelete,

        #endregion
    }

    public enum GuildMembersType
    {
        #region GuildMembers

        /// <summary>
        /// 当成员加入时
        /// </summary>
        GuildMemberAdd = 200,

        /// <summary>
        /// 当成员资料变更时
        /// </summary>
        GuildMemberUpdate,

        /// <summary>
        /// 当成员被移除时
        /// </summary>
        GuildMemberRemove,

        #endregion
    }

    public enum GuildMessagesType
    {
        #region GuildMessages

        /// <summary>
        /// 发送消息事件，代表频道内的全部消息
        /// </summary>
        MessageCreate = 300,

        /// <summary>
        /// 删除（撤回）消息事件
        /// </summary>
        MessageDelete,

        #endregion
    }

    public enum GuildMessageReactionsType
    {
        #region GuildMessageReactions

        /// <summary>
        /// 为消息添加表情表态
        /// </summary>
        MessageReactionAdd = 400,

        /// <summary>
        /// 为消息删除表情表态
        /// </summary>
        MessageReactionRemove,

        #endregion
    }

    public enum DirectMessageType
    {
        #region DirectMessage

        /// <summary>
        /// 当收到用户发给机器人的私信消息时
        /// </summary>
        DirectMessageCreate = 500,

        /// <summary>
        /// 删除（撤回）私信消息事件
        /// </summary>
        DirectMessageDelete,

        #endregion
    }

    public enum GroupAndC2cEventType
    {
        #region GroupAndC2cEvent

        /// <summary>
        /// 用户单聊发消息给机器人时候
        /// </summary>
        C2cMessageCreate = 600,

        /// <summary>
        /// 用户添加使用机器人
        /// </summary>
        FriendAdd,

        /// <summary>
        /// 用户删除机器人
        /// </summary>
        FriendDel,

        /// <summary>
        /// 用户在机器人资料卡手动关闭"主动消息"推送
        /// </summary>
        C2cMsgReject,

        /// <summary>
        /// 用户在机器人资料卡手动开启"主动消息"推送开关
        /// </summary>
        C2cMsgReceive,

        /// <summary>
        /// 用户在群里@机器人时收到的消息
        /// </summary>
        GroupAtMessageCreate,

        /// <summary>
        /// 机器人被添加到群聊
        /// </summary>
        GroupAddRobot,

        /// <summary>
        /// 机器人被移出群聊
        /// </summary>
        GroupDelRobot,

        /// <summary>
        /// 群管理员主动在机器人资料页操作关闭通知
        /// </summary>
        GroupMsgReject,

        /// <summary>
        /// 群管理员主动在机器人资料页操作开启通知
        /// </summary>
        GroupMsgReceive,

        #endregion
    }

    public enum InteractionType
    {
        #region Interaction

        /// <summary>
        /// 互动事件创建时
        /// </summary>
        InteractionCreate = 700,

        #endregion
    }

    public enum MessageAuditType
    {
        #region MessageAudit

        /// <summary>
        /// 消息审核通过
        /// </summary>
        MessageAuditPass = 800,

        /// <summary>
        /// 消息审核不通过
        /// </summary>
        MessageAuditReject,

        #endregion
    }

    public enum ForumsEvent
    {
        #region ForumsEvent

        /// <summary>
        /// 当用户创建主题时
        /// </summary>
        ForumThreadCreate = 900,

        /// <summary>
        /// 当用户更新主题时
        /// </summary>
        ForumThreadUpdate,

        /// <summary>
        /// 当用户删除主题时
        /// </summary>
        ForumThreadDelete,

        /// <summary>
        /// 当用户创建帖子时
        /// </summary>
        ForumPostCreate,

        /// <summary>
        /// 当用户删除帖子时
        /// </summary>
        ForumPostDelete,

        /// <summary>
        /// 当用户回复评论时
        /// </summary>
        ForumReplyCreate,

        /// <summary>
        /// 当用户删除回复时
        /// </summary>
        ForumReplyDelete,

        /// <summary>
        /// 当用户发表审核通过时
        /// </summary>
        ForumPublishAuditResult,

        #endregion
    }

    public enum AudioAction
    {
        #region AudioAction

        /// <summary>
        /// 音频开始播放时
        /// </summary>
        AudioStart = 1000,

        /// <summary>
        /// 音频播放结束时
        /// </summary>
        AudioFinish,

        /// <summary>
        /// 上麦时
        /// </summary>
        AudioOnMic,

        /// <summary>
        /// 下麦时
        /// </summary>
        AudioOffMic,

        #endregion
    }

    public enum PublicGuildMessages
    {
        #region PublicGuildMessages

        /// <summary>
        /// 当收到@机器人的消息时
        /// </summary>
        AtMessageCreate = 1100,

        /// <summary>
        /// 当频道的消息被删除时
        /// </summary>
        PublicMessageDelete

        #endregion
    }
}
