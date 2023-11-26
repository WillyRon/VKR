using Forest_fire_control.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forest_fire_control.Data.Model
{
    public class UserModel
    {
        public UserRoleEnum Role { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MiddleName { get; set; }
        public string Region { get; set; }
    }
}
