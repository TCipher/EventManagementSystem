
using EventHub.TicketManagement.Application.Contracts.Infrastructure;
using EventHub.TicketManagement.Application.Model.Mail;
using EventHub.TicketManagement.Infrastructure.FileExport;
using EventHub.TicketManagement.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.TicketManagement.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var emailConfig = configuration
            .GetSection("EmailSettings")
            .Get<EmailSettings>();
            services.AddSingleton(emailConfig);

            services.AddTransient<ICsvExporter, CsvExporter>();
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
