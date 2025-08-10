using System;
using System.Collections.Generic;

namespace Mliybs.QQBot.Data
{
    public record UserOpenId
    {
        public UserOpenId(string openId)
        {
            OpenId = openId;
        }

        public string OpenId { get; }
    }

    public record GroupOpenId
    {
        public GroupOpenId(string openId)
        {
            OpenId = openId;
        }

        public string OpenId { get; }
    }
}
