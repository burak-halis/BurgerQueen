using BurgerQueen.Services.Abstracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Concretes
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SmtpEmailSender> _logger;

        public SmtpEmailSender(IConfiguration configuration, ILogger<SmtpEmailSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtpServer = _configuration["SmtpSettings:Server"];
            var smtpPort = int.Parse(_configuration["SmtpSettings:Port"]);
            var smtpUsername = _configuration["SmtpSettings:Username"];
            var smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? throw new Exception("SMTP_PASSWORD ortam değişkeni ayarlanmamış.");
            var senderEmail = _configuration["SmtpSettings:SenderEmail"];

            try
            {
                using (var smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                    using (var message = new MailMessage())
                    {
                        message.From = new MailAddress(senderEmail);
                        message.To.Add(new MailAddress(email));
                        message.Subject = subject;
                        message.Body = htmlMessage;
                        message.IsBodyHtml = true;

                        await smtpClient.SendMailAsync(message);
                    }
                }

                _logger.LogInformation($"E-posta {email} adresine başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"E-posta gönderilirken hata oluştu. Email: {email}, Hata: {ex.Message}");
                throw; // Hata yukarıdaki katmana aktarılıyor, böylece işlem durdurulup kullanıcıya bildirilebilir
            }
        }
    }
}