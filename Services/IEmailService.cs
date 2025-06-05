using System.Threading.Tasks;

namespace TH_WEB.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
} 