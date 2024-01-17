using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using StackExchange.Redis;
using TesteKonsi.Application.Contracts;
using TesteKonsi.Application.Contracts.Messaging.Common;
using TesteKonsi.Application.Contracts.Repository.Cache;
using TesteKonsi.Domain.Contracts.Infra.Services;
using TesteKonsi.Domain.Contracts.Infra.Services.Messaging.Consumers;
using TesteKonsi.Domain.Contracts.Infra.Services.Messaging.Publishers;
using TesteKonsi.Infra.Services.Cache;
using TesteKonsi.Infra.Services.ElasticSearch;
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
        services.AddRedis(configuration);

        //ElasticSearch
        services.AddElasticSearch(configuration);
        services.AddTransient<IElasticSearch, UserBenefitsElasticSearch>();
        
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

    public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer
            .Connect(configuration
                .GetSection("RedisConfigurations:ConnectionString").Value);
        services.AddSingleton(connectionMultiplexer);
        services.AddStackExchangeRedisCache(options =>
        {
            options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer);
        });
        services.AddTransient<ICacheRepository, RedisRepository>();
    }

    public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
    {
        var settings =
            new ConnectionSettings(new Uri(configuration.GetSection("ElasticsearchConfigurations:Uri").Value));

        var defaultIndex = configuration.GetSection("ElasticsearchConfigurations:DefaultIndex").Value;

        if (!string.IsNullOrEmpty(defaultIndex))
            settings = settings.DefaultIndex(defaultIndex);

        var basicAuthUser = configuration.GetSection("ElasticsearchConfigurations:Username").Value;
        var basicAuthPassword = configuration.GetSection("ElasticsearchConfigurations:Password").Value;
        
        if (!string.IsNullOrEmpty(basicAuthUser) && !string.IsNullOrEmpty(basicAuthPassword))
            settings = settings.BasicAuthentication(basicAuthUser, basicAuthPassword);
        
        var client = new ElasticClient(settings);
        
        services.AddSingleton<IElasticClient>(client);
    }
}