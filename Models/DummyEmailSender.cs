using Microsoft.AspNetCore.Identity.UI.Services;

namespace MusicaNobaMVC.Models
{
    public class DummyEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Dummy implementation: Log the email details to the console
            Console.WriteLine($"Sending Email to: {email}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {htmlMessage}");
            return Task.CompletedTask;
        }

    }
}
