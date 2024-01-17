using RabbitMQ.Client;

namespace TesteKonsi.Application.Contracts.Messaging.Common;

public interface IRabbitMqService
{
    IConnection CreateChannel();
}