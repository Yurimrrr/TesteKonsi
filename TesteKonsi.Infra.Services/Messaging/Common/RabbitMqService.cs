using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using TesteKonsi.Application.Contracts.Messaging.Common;
using TesteKonsi.Infra.Services.Configurations;

namespace TesteKonsi.Infra.Services.Messaging.Common;

public class RabbitMqService : IRabbitMqService
{
    private readonly RabbitMqConfigurations _configuration;

    public RabbitMqService(IOptions<RabbitMqConfigurations> configuration)
    {
        _configuration = configuration.Value;
    }

    public IConnection CreateChannel()
    {
        var connection = new ConnectionFactory()
        {
            HostName = _configuration.HostName,
            Port = _configuration.Port,
            UserName = _configuration.UserName,
            Password = _configuration.Password,
            VirtualHost = _configuration.VHost,
            DispatchConsumersAsync = true,
            AutomaticRecoveryEnabled = true,
            RequestedHeartbeat = TimeSpan.FromSeconds(60)
        };

        // create connection
        connection.DispatchConsumersAsync = true;
        var _channel = connection.CreateConnection();
        return _channel;
    }
}