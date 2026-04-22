using EmailSender.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSender.Domain.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(Email email, CancellationToken cancellationToken = default);
    }
}
