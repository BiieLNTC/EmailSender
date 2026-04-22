using EmailSender.Domain.Interfaces;
using EmailSender.Infrastructure.Services;
using EmailSender.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailSender.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<SmtpSettings>(configuration.GetSection(SmtpSettings.SectionName));
            services.AddTransient<IEmailSender, SmtpEmailSender>();
            return services;
        }
    }
}
