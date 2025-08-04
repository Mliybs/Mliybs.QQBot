using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mliybs.QQBot.Data
{
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
    }
}
