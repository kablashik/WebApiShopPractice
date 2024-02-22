using Quartz;
using WebApplicationL5.Data.Email;

namespace WebApplicationL5.Jobs;

public class EmailSender :IJob
{
    private readonly IEmailSender _emailSender;

    public EmailSender(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }
    
    public async Task Execute(IJobExecutionContext context)
    {
        await _emailSender.SendEmailAsync("Email is sent");
    }
}