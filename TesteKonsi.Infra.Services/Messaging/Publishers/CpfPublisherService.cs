using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TesteKonsi.Application.Contracts.Messaging.Common;
using TesteKonsi.Domain.Contracts.Infra.Services.Messaging.Publishers;
using TesteKonsi.Infra.Services.Configurations;

namespace TesteKonsi.Infra.Services.Messaging.Publishers;

public class CpfPublisherService : ICpfPublisherService, IDisposable
{
    private IConnection _connection;
    private readonly RabbitMqConfigurations _rabbitMqConfigurations;
    private IModel _channel;
    private readonly IRabbitMqService _rabbitMqService;

    public CpfPublisherService(IOptions<RabbitMqConfigurations> rabbitMqConfigurations, IRabbitMqService rabbitMqService)
    {
        _rabbitMqConfigurations = rabbitMqConfigurations.Value;
        _rabbitMqService = rabbitMqService;
        InitConsumer();
    }

    private void InitConsumer()
    {
        _connection = _rabbitMqService.CreateChannel();

        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: _rabbitMqConfigurations.QueueCpf,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
    }

    public async Task PublishMessage(string req)
    {
        var message = JsonConvert.SerializeObject(req);
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish("", routingKey: _rabbitMqConfigurations.QueueCpf, basicProperties: null, body: body);
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

    public void Dispose()
    {
        if (_channel.IsOpen)
            _channel.Close();
        if (_connection.IsOpen)
            _connection.Close();
    }
}