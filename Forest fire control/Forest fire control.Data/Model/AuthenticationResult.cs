using System;
using System.Collections.Generic;
using System.Text;

namespace Forest_fire_control.Data.Model
{
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
    }
}
