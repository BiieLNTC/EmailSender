using EmailSender.Application.Dtos;
using EmailSender.Domain.Entities;
using EmailSender.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace EmailSender.Application.UseCases
{
    public class SendEmailUseCase(
    IEmailSender emailSender,
    ILogger<SendEmailUseCase> logger)
    {
        public async Task ExecuteAsync(
            SendEmailRequest request,
            CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Sending email to {Recipients}", string.Join(", ", request.To));

            var email = Email.Create(
                from: request.From,
                to: request.To,
                subject: request.Subject,
                body: request.Body,
                isHtml: request.IsHtml,
                cc: request.Cc,
                bcc: request.Bcc
            );

            await emailSender.SendAsync(email, cancellationToken);

            logger.LogInformation("Email sent successfully to {Recipients}", string.Join(", ", request.To));
        }

    }
}
