namespace TesteKonsi.Infra.Services.Configurations;

public class RabbitMqConfigurations
{
    public string HostName { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string QueueCpf { get; set; }
    public string VHost { get; set; }
}