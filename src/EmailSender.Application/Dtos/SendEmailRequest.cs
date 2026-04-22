using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSender.Application.Dtos
{
    public record SendEmailRequest(
        string From,
        List<string> To,
        string Subject,
        string Body,
        bool IsHtml = false,
        List<string>? Cc = null,
        List<string>? Bcc = null
    );
}
