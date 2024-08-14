namespace Expense.Commons.Email;

public interface IEmailService
{
    public Task SendEmailAsync(string toEmail, string subject, string message);

}