using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly ISendGridClient _sendGridClient;
        private readonly ILogger<EmailSender> _logger;


        public EmailSender(IConfiguration configuration,
            ISendGridClient sendGridClient, ILoggerFactory logger){
            _configuration  = configuration;
            _sendGridClient = sendGridClient;
            _logger = logger.CreateLogger<EmailSender>();
        }
        
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            _logger.LogInformation("SendEmail called.");
            
            var apiKey = _configuration.GetValue<string>("SendGrid:apiKey");

            var from = new EmailAddress(_configuration.GetValue<string>("SendGrid:from"));

            var to   = new EmailAddress(email);
            var plainTextContent = string.Empty;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, message);

            var client = new SendGridClient(apiKey);
            var response = await client.SendEmailAsync(msg);

            if(response.StatusCode == HttpStatusCode.Accepted){
                _logger.LogInformation($"E-mail sended to {email}");
            }

            else
            {
                _logger.LogError($"ERROR Sending email to {email} {response.ToString()}");
                throw new Exception(response.ToString());
            }
        }
    }
}
