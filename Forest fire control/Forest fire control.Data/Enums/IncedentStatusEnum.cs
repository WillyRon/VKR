using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Forest_fire_control.Data.Enums
{
    public enum IncedentStatusEnum
    {
        [Description("Новый")]
        New = 1,

        [Description("Обработан")]
        Done = 2,
    }
}
