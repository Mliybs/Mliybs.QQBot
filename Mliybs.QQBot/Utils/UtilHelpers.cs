using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Mliybs.QQBot.Data;
using Mliybs.QQBot.Data.Attributes;

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
    }
}
