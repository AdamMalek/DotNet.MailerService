using Codibly.Services.Mailer.Domain.Repositories;
using Codibly.Services.Mailer.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Codibly.Services.Mailer.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IEmailRepository, EmailRepository>();
            
            return serviceCollection;
        }
    }
}