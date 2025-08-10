using Mliybs.QQBot.Data;
using Mliybs.QQBot.Data.Attributes;
using Org.BouncyCastle.Utilities;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mliybs.QQBot.Utils
{
    public static class UtilHelpers
    {
        public static readonly JsonSerializerOptions Options = new()
        {
            MaxDepth = 128,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            Converters =
            {
                new JsonStringEnumConverter<EventType>(JsonNamingPolicy.SnakeCaseUpper),
                new AttachmentConverter(),
                new UserOpenIdConverter(),
                new GroupOpenIdConverter(),
            }
        };

        public static readonly ConcurrentDictionary<EventType, Type> EventTypes;

        static UtilHelpers()
        {
            EventTypes = new(Assembly.GetAssembly(typeof(UtilHelpers))!.GetTypes()
                .Select(x => (Event: x, Attribute: x.GetCustomAttribute<EventAttribute>()))
                .Where(x => x.Attribute != null)
                .Select(x => new KeyValuePair<EventType, Type>(x.Attribute!.Type, x.Event)));
        }

        /// <summary>
        /// 返回小写十六进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] bytes)
#if NETSTANDARD2_1
        {
            var builder = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
#else
        {
            return Convert.ToHexString(bytes).ToLower();
        }
#endif

#if NETSTANDARD2_1
        public static string GetString(this Encoding encoding, ReadOnlySequence<byte> bytes)
        {
            return encoding.GetString(bytes.ToArray());
        }
#endif
    }
}
