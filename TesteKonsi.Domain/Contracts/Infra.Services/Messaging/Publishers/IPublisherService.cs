namespace TesteKonsi.Domain.Contracts.Infra.Services.Messaging.Publishers;

public interface IPublisherService<T>
{
    Task PublishMessage(T t);
}