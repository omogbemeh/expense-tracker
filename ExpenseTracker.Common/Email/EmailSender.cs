using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace ExpenseTracker.Common.Email;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        this._emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        MailMessage mailMessage = new MailMessage
        {
            From = new MailAddress(_emailSettings.FromEmail),
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };
        
        mailMessage.To.Add(new MailAddress(toEmail));

        using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort);
        client.Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);

        await client.SendMailAsync(mailMessage);
    }
}