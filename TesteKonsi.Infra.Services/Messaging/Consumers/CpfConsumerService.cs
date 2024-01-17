using System.Text;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TesteKonsi.Application.Contracts.Messaging.Common;
using TesteKonsi.Application.DTOs.Request;
using TesteKonsi.Domain.Contracts.Infra.Services.Messaging.Consumers;
using TesteKonsi.Infra.Services.Configurations;

namespace TesteKonsi.Infra.Services.Messaging.Consumers;

public class CpfConsumerService : ICpfConsumerService, IDisposable
{
    private IConnection _connection;
    private readonly RabbitMqConfigurations _rabbitMqConfigurations;
    private IModel _channel;
    private readonly IMediator _mediator;
    private readonly IRabbitMqService _rabbitMqService;

    public CpfConsumerService(IOptions<RabbitMqConfigurations> rabbitMqConfigurations, 
        IMediator mediator, IRabbitMqService rabbitMqService)
    {
        _rabbitMqConfigurations = rabbitMqConfigurations.Value;
        _mediator = mediator;
        _rabbitMqService = rabbitMqService;
        InitConsumer();
    }

    private void InitConsumer()
    {
        _connection = _rabbitMqService.CreateChannel();

        // create channel
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: _rabbitMqConfigurations.QueueCpf,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
    }

    public async Task ReadMessages()
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        var content = "";

        consumer.Received += async (sender, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            content = Encoding.UTF8.GetString(body);

            try
            {
                var cpf = JsonConvert.DeserializeObject<string>(content);
                
                var command = new BenefitsRequestDTO(cpf);
                var result = await _mediator.Send(command);
                // ** AQUI VAI SER A REGRA DE VALIDAR SE EXISTE O CPF NO REDIS, SE NÃO ADICIONA E DEPOIS COLOCA NO
                // ** ELASTICSEARCH.
                
                // var retorno = await _createDocumentService.CreateDocumentService(createDocumentInputModel);
                //
                // if (!retorno.IsNullOrEmpty())
                // {
                //     var req = new VerifyResultDocumentSenderDTO(retorno, createDocumentInputModel.IdProjetoEtapa);
                //     await _verifyResultDocumentPublisherService.PublishMessage(req);
                // }

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        };

        _channel.BasicConsume(_rabbitMqConfigurations.QueueCpf, false, consumer);
        await Task.CompletedTask;
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