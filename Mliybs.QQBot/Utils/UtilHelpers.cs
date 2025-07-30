using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mliybs.QQBot.Utils
{
    public static class UtilHelpers
    {
        public static readonly JsonSerializerOptions Options = new()
        {
            MaxDepth = 128,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
    }
}
