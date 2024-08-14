using Microsoft.AspNetCore.Identity.UI.Services;

namespace ExpenseTracker.Commons.Email;

public class EmailSender : IEmailSender
{
    private readonly EmailService _emailService;

    public EmailSender(EmailService emailService)
    {
        _emailService = emailService;
    }
    
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        await this._emailService.SendEmailAsync(email, subject, htmlMessage);
    }
}