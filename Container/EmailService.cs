using LearnAPI.Modal;
using LearnAPI.Service;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace LearnAPI.Container
{
    // Implementation of the email service interface
    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings;
        // Constructor to initialize the service with necessary email setting
        public EmailService(IOptions<EmailSettings> options) {
          this.emailSettings = options.Value;
        }

        // Method to send an email
        public async Task SendEmail(Mailrequest mailrequest)
        {
            var email = new MimeMessage();
            // Set the sender's email address
            email.Sender = MailboxAddress.Parse(emailSettings.Email);
            // Add the recipient's email address
            email.To.Add(MailboxAddress.Parse(mailrequest.Email));
            // Set the subject of the email
            email.Subject=mailrequest.Subject;
            var builder=new BodyBuilder();
            // Set the HTML body of the email
            builder.HtmlBody = mailrequest.Emailbody;
            email.Body = builder.ToMessageBody();

            using var smptp = new SmtpClient();
            // Connect to the SMTP server using the specified host and port with secure options
            smptp.Connect(emailSettings.Host, emailSettings.Port,SecureSocketOptions.StartTls);
            // Authenticate using the provided email and password
            smptp.Authenticate(emailSettings.Email, emailSettings.Password);
            // Send the email asynchronously
            await smptp.SendAsync(email);
            // Disconnect from the SMTP server
            smptp.Disconnect(true);
        }
    }
}
