using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Application.Commands;
using Codibly.Services.Mailer.Application.Dto;
using Codibly.Services.Mailer.Application.Services;
using Codibly.Services.Mailer.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codibly.Services.Mailer.Host.HostedServices
{
    public class EmailSenderHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IMediator mediator;
        private readonly IEmailSender sender;
        private readonly IEmailQueueRepository emailRepository;

        public EmailSenderHostedService(ILogger<EmailSenderHostedService> logger, IMediator mediator,
            IEmailSender sender, IEmailQueueRepository emailRepository)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.sender = sender;
            this.emailRepository = emailRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                try
                {
                    var messages = (await this.emailRepository.GetQueuedMessages()).ToList();
                    if (messages.Any())
                    {
                        var publishTasks = messages.Select(em => this.sender.SendAsync(new FinalizedEmailMessageDto(em))).ToArray();
                        await Task.WhenAll(publishTasks);
                        await this.mediator.Send(new MarkMessagesAsSent(messages.Select(x => x.Id)), stoppingToken);
                        this.logger.LogInformation($"Sent {publishTasks.Length} message(s)");
                    }
                    else
                    {
                        this.logger.LogInformation("No messages to send");
                    }
                }
                catch (Exception e)
                {
                    this.logger.LogError(e, "Error occured while sending emails");
                }
                finally
                {
                    await Task.Delay(2000, stoppingToken);
                }
            }
        }
    }
}