using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Forest_fire_control.BI.ServiceInterfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
