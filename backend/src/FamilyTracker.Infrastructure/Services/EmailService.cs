using FamilyTracker.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace FamilyTracker.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendShoppingListEmailAsync(
        string toEmail, 
        string userName, 
        IEnumerable<string> shoppingItems, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var emailSettings = _configuration.GetSection("EmailSettings");
            var smtpHost = emailSettings["SmtpHost"];
            var smtpPort = int.Parse(emailSettings["SmtpPort"] ?? "587");
            var smtpUser = emailSettings["SmtpUser"];
            var smtpPassword = emailSettings["SmtpPassword"];
            var fromEmail = emailSettings["FromEmail"];
            var fromName = emailSettings["FromName"] ?? "Family Tracker";

            var itemsList = string.Join("\n", shoppingItems.Select((item, index) => $"{index + 1}. {item}"));
            
            var emailBody = $@"
Hello {userName},

Here is the shopping list:

{itemsList}

Best regards,
Family Tracker
";

            // Demo mode: If SMTP is not configured, log the email instead of sending
            if (string.IsNullOrEmpty(smtpHost) || string.IsNullOrEmpty(smtpUser) || string.IsNullOrEmpty(smtpPassword))
            {
                _logger.LogWarning("=== EMAIL DEMO MODE ===");
                _logger.LogWarning("SMTP not configured. Email content logged below:");
                _logger.LogWarning("To: {Email}", toEmail);
                _logger.LogWarning("From: {FromName} <{FromEmail}>", fromName, fromEmail ?? "noreply@familytracker.com");
                _logger.LogWarning("Subject: Shopping List from Family Tracker");
                _logger.LogWarning("Body:\n{Body}", emailBody);
                _logger.LogWarning("=== END EMAIL DEMO ===");
                _logger.LogInformation("Shopping list email logged (demo mode) for {Email}", toEmail);
                return;
            }

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail ?? smtpUser, fromName),
                Subject = "Shopping List from Family Tracker",
                Body = emailBody,
                IsBodyHtml = false
            };

            mailMessage.To.Add(toEmail);

            using var smtpClient = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPassword),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(mailMessage, cancellationToken);
            _logger.LogInformation("Shopping list email sent to {Email}", toEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send shopping list email to {Email}", toEmail);
            throw;
        }
    }
}
