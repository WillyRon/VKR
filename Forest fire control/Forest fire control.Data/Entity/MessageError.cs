using System;
using System.Collections.Generic;
using System.Text;

namespace Forest_fire_control.Data.Models
{
    public class MessageError
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Message { get; set; }

        public User User { get; set; }
    }
}
