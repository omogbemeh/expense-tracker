namespace ExpenseTracker.Common.Email;

public interface IEmailService
{
    public Task SendEmailAsync(string toEmail, string subject, string message);

}