namespace WebApplicationL5.Data.Email;

public interface IEmailSender
{
    Task<string> SendEmailAsync(string recipientEmail);
}