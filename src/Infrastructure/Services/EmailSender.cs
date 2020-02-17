using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<EmailSender> _logger;


        public EmailSender(IConfiguration configuration,
            ISendGridClient sendGridClient, IServiceProvider serviceProvider, ILoggerFactory logger){
            _configuration  = configuration;
            _sendGridClient = sendGridClient;
            _serviceProvider = serviceProvider;
            _logger = logger.CreateLogger<EmailSender>();
        }
        
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var configuration = _serviceProvider.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>("SendGrid:apiKey");
            var client = new SendGridClient(apiKey);

            if(string.IsNullOrEmpty(apiKey)){
                throw new Exception("SendGrid apiKey is null or empty");
            }

            var from = new EmailAddress(_configuration.GetValue<string>("SendGrid:from"));

            var to   = new EmailAddress(email);
            var plainTextContent = string.Empty;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, message);

            var response = await _sendGridClient.SendEmailAsync(msg);

            if(response.StatusCode == HttpStatusCode.Accepted){
                  _logger.LogInformation($"Send e-mail to {email} is confirmed.");
            }

            else
            {
                _logger.LogError($"Send e-mail to {email} is not confirmed. {response.ToString()}");
                throw new Exception(response.ToString());
            }
        }
    }
}
