using Dapr.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PokEBay.Notifications.API.Infrastructure.DTO;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace PokEBay.Notifications.API.Infrastructure
{
    public class NotificationService : INotificationService
    {
        DaprClient _daprClient;
        IConfiguration _configuration;
        ILogger<NotificationService> _logger;

        public NotificationService(DaprClient daprClient, IConfiguration configuration, ILogger<NotificationService> logger)
        {
            _daprClient = daprClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task NotifyAsync(OrderDto orderDto)
        {
            // Send notification here.
            await Task.Run(() => _logger.LogInformation($"***Your dummy email.*** \n Order# {orderDto.Id} was successfully placed. \n *****"));

            try
            {
                var apiKey = "[SendGrid Key]";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("test@example.com", "Example User");
                var subject = "Sending with SendGrid is Fun";
                var to = new EmailAddress("test@example.com", "Example User");
                var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Sending email to endGrid failed with the following error(s): \n {ex.Message} \n {ex.InnerException?.Message}");
            }
        }
    }
}
