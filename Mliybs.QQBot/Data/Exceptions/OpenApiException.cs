using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable
namespace Mliybs.QQBot.Data.Exceptions
{
    public class OpenApiException : Exception
    {
        [Obsolete("不应使用")]
        public OpenApiException() { }

        [Obsolete("不应使用")]
        public OpenApiException(string message) : base(message) { }

        public OpenApiException(OpenApiExceptionContext context) : base(context.Message)
        {
            Context = context;
        }

        public OpenApiExceptionContext Context { get; }
    }

    public record OpenApiExceptionContext
    {
        [JsonPropertyName("code")]
        public OpenApiErrorCode ErrorCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

#nullable restore
        /// <summary>
        /// 官方文档中返回体不含有trace_id，经实际测试决定加上该属性
        /// </summary>
        [JsonPropertyName("trace_id")]
        public string? TraceId { get; set; }
    }
    public enum OpenApiErrorCode
    {
        Ok = 0,

        UnknownAccount = 10001,

        UnknownChannel = 10003,

        UnknownGuild = 10004,

        ErrorCheckAdminFailed = 11281,

        ErrorCheckAdminNotPass = 11282,

        ErrorWrongAppId = 11251,

        ErrorCheckAppPrivilegeFailed = 11252,

        ErrorCheckAppPrivilegeNotPass = 11253,

        ErrorInterfaceForbidden = 11254,

        ErrorWrongAppId2 = 11261,

        ErrorCheckRobot = 11262,

        ErrorCheckGuildAuth = 11263,

        ErrorGuildAuthNotPass = 11264,

        ErrorRobotHasBanned = 11265,

        ErrorWrongToken = 11241,

        ErrorCheckTokenFailed = 11242,

        ErrorCheckTokenNotPass = 11243,

        ErrorCheckUserAuth = 11273,

        ErrorUserAuthNotPass = 11274,

        ErrorWrongAppId3 = 11275,

        ErrorGetHttpHeader = 11301
    }
}
