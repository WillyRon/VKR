using Forest_fire_control.BI.ServiceInterfaces;
using Forest_fire_control.Data.Model;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

public class MailgunEmailSender : IEmailSender, IDisposable
{
    private readonly SmtpClient _smtpClient;
    private readonly string _fromName;

    public MailgunEmailSender(IOptions<MailgunSettings> mailgunSettings)
    {
        var settings = mailgunSettings.Value;

        _smtpClient = new SmtpClient();
        _smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

        try
        {
            _smtpClient.Connect(settings.SmtpHost, settings.SmtpPort, SecureSocketOptions.StartTls);
            _smtpClient.Authenticate(settings.SmtpUsername, settings.ApiKey);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to SMTP server: {ex.Message}");
        }

        _fromName = settings.FromName;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(_fromName, "willi.weeni@gmail.com")); // Используйте реальный адрес отправителя
        mimeMessage.To.Add(new MailboxAddress("", email));
        mimeMessage.Subject = subject;

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.TextBody = message;
        mimeMessage.Body = bodyBuilder.ToMessageBody();

        try
        {
            await _smtpClient.SendAsync(mimeMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }

    public void Dispose()
    {
        _smtpClient.Disconnect(true);
        _smtpClient.Dispose();
    }
}