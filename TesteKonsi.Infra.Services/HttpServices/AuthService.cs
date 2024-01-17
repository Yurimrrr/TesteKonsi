using System.Text.Json;
using TesteKonsi.Domain.Contracts.Infra.Services;
using TesteKonsi.Domain.ValueObjects;
using TesteKonsi.Domain.ValueObjects.Response.Auth;

namespace TesteKonsi.Infra.Services.HttpServices;
public class AuthService : IAuthService
{
    private readonly IHttpRequestService _httpService;

    public AuthService(IHttpRequestService httpService)
    {
        _httpService = httpService;
    }

    public async Task<AuthResponse> Auth(AuthCommand request)
    {
        var requestJson = JsonSerializer.Serialize(request);
        
        //jogar a request pro config
        var result = await _httpService.HttpRequest<AuthResponse>(
            "http://teste-dev-api-dev-140616584.us-east-1.elb.amazonaws.com",
            "/api/v1/token",
            requestJson,
            HttpMethod.Post
        );

        return result;
    }
}