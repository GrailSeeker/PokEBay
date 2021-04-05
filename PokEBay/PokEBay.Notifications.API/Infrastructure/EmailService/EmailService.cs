using Microsoft.Extensions.Logging;
using PokEBay.Notifications.API.Infrastructure.DTO;
using System.Threading.Tasks;

namespace PokEBay.Notifications.API.Infrastructure.EmailService
{
    public class EmailService : INotificationService
    {
        ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task NotifyAsync(OrderDto orderDto)
        {
            // TODO:
            // Send Email here
            _logger.LogInformation("Sending email...");
        }
    }
}
