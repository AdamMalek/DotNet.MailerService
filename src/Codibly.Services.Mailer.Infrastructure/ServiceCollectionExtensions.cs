using Codibly.Services.Mailer.Application.Services;
using Codibly.Services.Mailer.Domain.Repositories;
using Codibly.Services.Mailer.Infrastructure.Options;
using Codibly.Services.Mailer.Infrastructure.Repositories;
using Codibly.Services.Mailer.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Codibly.Services.Mailer.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection,
            MongoConnectionString mongoOptions, EmailSenderOptions emailSenderOptions)
        {
            serviceCollection.AddSingleton(mongoOptions);
            serviceCollection.AddTransient<IEmailRepository, EmailRepository>();
            serviceCollection.AddTransient<IEmailQueueRepository, EmailRepository>();
            serviceCollection.AddTransient<IEmailSender, EmailSender>(provider => new EmailSender(emailSenderOptions));

            return serviceCollection;
        }
    }
}