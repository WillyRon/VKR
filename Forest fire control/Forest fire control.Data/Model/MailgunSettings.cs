using System;
using System.Collections.Generic;
using System.Text;

namespace Forest_fire_control.Data.Model
{
    public class MailgunSettings
    {
        public string ApiKey { get; set; }
        public string Domain { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string FromName { get; set; }
    }
}
