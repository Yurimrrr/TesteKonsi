using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using TesteKonsi.Application.Contracts.Messaging.Common;
using TesteKonsi.Application.Contracts.Repository.Cache;
using TesteKonsi.Domain.Contracts.Infra.Services;
using TesteKonsi.Domain.Contracts.Infra.Services.Messaging.Consumers;
using TesteKonsi.Domain.Contracts.Infra.Services.Messaging.Publishers;
using TesteKonsi.Infra.Services.Cache;
using TesteKonsi.Infra.Services.HttpServices;
using TesteKonsi.Infra.Services.Messaging.Common;
using TesteKonsi.Infra.Services.Messaging.Consumers;
using TesteKonsi.Infra.Services.Messaging.Consumers.HostedServices;
using TesteKonsi.Infra.Services.Messaging.Publishers;
using TesteKonsi.Infra.Services.Messaging.Publishers.HostedServices;
using TesteKonsi.Infra.Services.Utils;

namespace TesteKonsi.Infra.Services.IOC;

public static class Bootstrap
{
    public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // Services
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IBenefitsService, BenefitsConsultService>();
        services.AddTransient<IHttpRequestService, HttpRequestService>();
        
        //Broker
        services.AddTransient<IRabbitMqService, RabbitMqService>();
        services.AddTransient<ICpfConsumerService, CpfConsumerService>();
        services.AddTransient<ICpfPublisherService, CpfPublisherService>();
        
        //Redis
        IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer
            .Connect(configuration
                .GetSection("RedisConfigurations:ConnectionString").Value);
        services.AddSingleton(connectionMultiplexer);
        services.AddStackExchangeRedisCache(options =>
        {
            options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer);
        });
        services.AddTransient<ICacheRepository, RedisRepository>();
        
        // Mediador 
        services.AddMediatR(config => config
            .RegisterServicesFromAssembly(
                AppDomain.CurrentDomain.Load("TesteKonsi.Application")
            )
        );
        
        //HostedServices
        services.AddHostedService<CpfPublisherHostedService>();
        services.AddHostedService<CpfConsumerHostedService>();

        return services;
    }
}