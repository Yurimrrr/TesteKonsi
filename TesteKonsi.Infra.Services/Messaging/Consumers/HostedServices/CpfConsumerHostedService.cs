using Microsoft.Extensions.Hosting;
using TesteKonsi.Domain.Contracts.Infra.Services.Messaging.Consumers;

namespace TesteKonsi.Infra.Services.Messaging.Consumers.HostedServices;

public class CpfConsumerHostedService : BackgroundService
{
    private readonly ICpfConsumerService _cpfConsumerService;

    public CpfConsumerHostedService(ICpfConsumerService cpfConsumerService)
    {
        _cpfConsumerService = cpfConsumerService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        await _cpfConsumerService.ReadMessages();
    }
}