using System.Threading.Tasks;

namespace TH_WEB.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body)
        {
            // This is a basic placeholder implementation.
            // In a real application, you would integrate with an email sending service like SendGrid, Mailgun, etc.
            // For now, we'll just simulate sending an email.
            Console.WriteLine($"Sending email to: {to}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");

            return Task.CompletedTask;
        }
    }
} 