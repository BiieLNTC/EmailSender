using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSender.Domain.Entities
{
    public class Email
    {
        public string From { get; private set; }
        public List<string> To { get; private set; }
        public List<string> Cc { get; private set; }
        public List<string> Bcc { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public bool IsHtml { get; private set; }

        private Email() { }

        public static Email Create(
            string from,
            List<string> to,
            string subject,
            string body,
            bool isHtml = false,
            List<string>? cc = null,
            List<string>? bcc = null) => new()
            {
                From = from,
                To = to,
                Subject = subject,
                Body = body,
                IsHtml = isHtml,
                Cc = cc ?? [],
                Bcc = bcc ?? []
            };
    }
}
