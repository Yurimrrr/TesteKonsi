namespace TesteKonsi.Domain.Contracts.Infra.Services;

public interface IHttpRequestService
{
    Task<T?> HttpRequest<T>(string url, string resource, string body, HttpMethod method, string? token = "");
}