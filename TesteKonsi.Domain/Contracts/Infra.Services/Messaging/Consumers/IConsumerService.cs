namespace TesteKonsi.Domain.Contracts.Infra.Services.Messaging.Consumers;

public interface IConsumerService
{
    Task ReadMessages();
}