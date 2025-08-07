using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mliybs.QQBot.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EventAttribute : Attribute
    {
        public EventAttribute(EventType type)
        {
            Type = type;
        }

        public EventType Type { get; set; }
    }
}
