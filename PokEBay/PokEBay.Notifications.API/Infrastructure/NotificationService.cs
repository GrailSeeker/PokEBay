using Dapr.Client;
using Microsoft.Extensions.Configuration;
using PokEBay.Notifications.API.Infrastructure.DTO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net.Mail;

namespace PokEBay.Notifications.API.Infrastructure
{
    public class NotificationService : INotificationService
    {
        DaprClient _daprClient;
        IConfiguration _configuration;

        public NotificationService(DaprClient daprClient, IConfiguration configuration)
        {
            _daprClient = daprClient;
            _configuration = configuration;
        }

        public async Task NotifyAsync(OrderDto orderDto)
        {
            // TODO: FIX THIS!!!!

            //// Send Email here
            //var message = $"Hi! \n Your order# {orderDto.Id} is successfully placed.";
            //var metadata = new Dictionary<string, string>
            //{
            //    {"emailFrom", "ordermaster@pokebay.com"},
            //    {"emailTo", "prdora@deloitte.com"},
            //    {"subject", $"Your pokebay order #{orderDto.Id}"}
            //};

            //await _daprClient.InvokeBindingAsync(_configuration["DaprConfiguration:EmailBindingStore"], "create", message, metadata, new CancellationToken());
            try
            {
                var host = "0.0.0.0";
                var port = 3000;
                var url = "http://smtp4dev:3000";

                using (var client = new SmtpClient(url))
                
                {
                    var mailMessage = new MailMessage("no-reply@pokebay.com", "prdora@deloite.com", $"Your order# {orderDto.Id}", $"Hi! \n Your order# {orderDto.Id} is successfully placed.");


                    await client.SendMailAsync(mailMessage);
                }

                //var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
                //var client = new SendGridClient("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");
                //var from = new EmailAddress("test@example.com", "Example User");
                //var subject = "Sending with SendGrid is Fun";
                //var to = new EmailAddress("test@example.com", "Example User");
                //var plainTextContent = "and easy to do anywhere, even with C#";
                //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                //var response = await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error sending email:\n {ex.Message} \n {ex.InnerException?.Message}");
            }

            await Task.Run(() => Debug.WriteLine($"Hi! \n Your order# {orderDto.Id} is successfully placed."));
        }
    }
}
