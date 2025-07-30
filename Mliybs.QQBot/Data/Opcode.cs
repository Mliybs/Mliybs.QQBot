using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mliybs.QQBot.Data
{
    /// <summary>
    /// <para>Receive 客户端接收到服务端 push 的消息</para>
    /// <para>Send 客户端发送消息</para>
    /// <para>Reply 客户端接收到服务端发送的消息之后的回包（HTTP 回调模式）</para>
    /// </summary>
    public enum Opcode
    {
        /// <summary>
        /// <para>服务端进行消息推送</para>
        /// <para>webhook/websocket Receive</para>
        /// </summary>
        Dispatch = 0,

        /// <summary>
        /// <para>客户端或服务端发送心跳</para>
        /// <para>websocket Send/Receive</para>
        /// </summary>
        Heartbeat = 1,

        /// <summary>
        /// <para>客户端发送鉴权</para>
        /// <para>websocket Send</para>
        /// </summary>
        Identify = 2,

        /// <summary>
        /// <para>客户端恢复连接</para>
        /// <para>websocket Send</para>
        /// </summary>
        Resume = 6,

        /// <summary>
        /// <para>服务端通知客户端重新连接</para>
        /// <para>websocket Receive</para>
        /// </summary>
        Reconnect = 7,

        /// <summary>
        /// <para>当 identify 或 resume 的时候，如果参数有错，服务端会返回该消息</para>
        /// <para>websocket Receive</para>
        /// </summary>
        InvalidSession = 9,

        /// <summary>
        /// <para>当客户端与网关建立 ws 连接之后，网关下发的第一条消息</para>
        /// <para>websocket Receive</para>
        /// </summary>
        Hello = 10,

        /// <summary>
        /// <para>当发送心跳成功之后，就会收到该消息</para>
        /// <para>websocket Receive/Reply</para>
        /// </summary>
        HeartbeatAck = 11,

        /// <summary>
        /// <para>仅用于 http 回调模式的回包，代表机器人收到了平台推送的数据</para>
        /// <para>webhook Reply</para>
        /// </summary>
        HttpCallbackAck = 12,

        /// <summary>
        /// <para>开放平台对机器人服务端进行验证</para>
        /// <para>webhook Receive</para>
        /// </summary>
        CallbackUrlValidate = 13,

        /// <summary>
        /// <para>开放平台对机器人服务端进行验证</para>
        /// <para>webhook Receive</para>
        /// </summary>
        回调地址验证 = 13
    }
}
