using EmailSender.Domain.Entities;
using EmailSender.Domain.Interfaces;
using EmailSender.Infrastructure.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace EmailSender.Infrastructure.Services
{
    public class SmtpEmailSender(IOptions<SmtpSettings> options) : IEmailSender
    {
        private readonly SmtpSettings _settings = options.Value;

        public async Task SendAsync(Email email, CancellationToken cancellationToken = default)
        {
            var message = BuildMessage(email);

            using var client = new SmtpClient();

            await client.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, cancellationToken);
            await client.AuthenticateAsync(_settings.Username, _settings.Password, cancellationToken);
            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }

        private static MimeMessage BuildMessage(Email email)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(email.From));
            email.To.ForEach(to => message.To.Add(MailboxAddress.Parse(to)));
            email.Cc.ForEach(cc => message.Cc.Add(MailboxAddress.Parse(cc)));
            email.Bcc.ForEach(bcc => message.Bcc.Add(MailboxAddress.Parse(bcc)));
            message.Subject = email.Subject;

            var body = new BodyBuilder();
            if (email.IsHtml) body.HtmlBody = email.Body;
            else body.TextBody = email.Body;

            message.Body = body.ToMessageBody();
            return message;
        }
    }
}
