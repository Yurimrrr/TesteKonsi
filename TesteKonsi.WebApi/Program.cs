
using TesteKonsi.Infra.Services.Configurations;
using TesteKonsi.Infra.Services.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

Console.WriteLine("CONFIGURATIONS ==========================================");
Console.WriteLine(builder.Configuration.GetSection("RabbitMqConfigurations:HostName").Value);
Console.WriteLine(builder.Configuration.GetSection("RabbitMqConfigurations:Port").Value);
Console.WriteLine(builder.Configuration.GetSection("RabbitMqConfigurations:UserName").Value);
Console.WriteLine(builder.Configuration.GetSection("RabbitMqConfigurations:Password").Value);
Console.WriteLine("CONFIGURATIONS ==========================================");

builder.Services.InjectDependencies(builder.Configuration);


builder.Services
    .Configure<RabbitMqConfigurations>(
        builder
            .Configuration
            .GetSection((nameof(RabbitMqConfigurations))
        )
    );

builder.Services
    .Configure<ApiBeneficios>(
        builder
            .Configuration
            .GetSection((nameof(ApiBeneficios))
            )
    );

builder.Services
    .Configure<RedisConfigurations>(
        builder
            .Configuration
            .GetSection((nameof(RedisConfigurations))
            )
    );

builder.Services
    .Configure<ElasticsearchConfigurations>(
        builder
            .Configuration
            .GetSection((nameof(ElasticsearchConfigurations))
            )
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
