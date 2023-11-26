using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Forest_fire_control.Data.Enums
{
    public enum UserRoleEnum
    {
        [Description("Диспетчер")]
        Dispatcher = 1,

        [Description("Администратор")]
        Admin = 2,
    }
}
