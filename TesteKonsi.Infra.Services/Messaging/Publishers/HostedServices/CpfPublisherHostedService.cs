using Microsoft.Extensions.Hosting;
using TesteKonsi.Domain.Contracts.Infra.Services.Messaging.Publishers;

namespace TesteKonsi.Infra.Services.Messaging.Publishers.HostedServices;

public class CpfPublisherHostedService : BackgroundService
{
    private readonly ICpfPublisherService _cpfPublisherService;

    public CpfPublisherHostedService(ICpfPublisherService cpfPublisherService)
    {
        _cpfPublisherService = cpfPublisherService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        List<String> listCpfs = new List<string>();

        for (int i = 0; i < 3; i++)
        {
            // listCpfs.Add("343.228.350-40");
            // listCpfs.Add("869.230.000-41");
            // listCpfs.Add("568.946.870-30");
            // listCpfs.Add("433.510.120-12");
            // listCpfs.Add("415.022.590-79");
        }

        foreach (var cpf in listCpfs)
        {
            await _cpfPublisherService.PublishMessage(cpf);
        }

    }
}