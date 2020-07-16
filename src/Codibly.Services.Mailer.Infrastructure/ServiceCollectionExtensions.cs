using Codibly.Services.Mailer.Application.Services;
using Codibly.Services.Mailer.Domain.Repositories;
using Codibly.Services.Mailer.Infrastructure.Repositories;
using Codibly.Services.Mailer.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Codibly.Services.Mailer.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IEmailRepository, EmailRepository>();
            serviceCollection.AddTransient<IEmailSender, EmailSender>();

            return serviceCollection;
        }
    }
}