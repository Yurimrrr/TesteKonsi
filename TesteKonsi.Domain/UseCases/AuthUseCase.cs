using TesteKonsi.Domain.Contracts.Infra.Services;
using TesteKonsi.Domain.Shared;
using TesteKonsi.Domain.ValueObjects;
using TesteKonsi.Domain.ValueObjects.Response;
using TesteKonsi.Domain.ValueObjects.Response.Auth;

namespace TesteKonsi.Domain.UseCases;

public class AuthUseCase : IUseCase<AuthCommand, AuthResponse>
{
    private readonly IAuthService _authService;

    public AuthUseCase(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponse> Execute(AuthCommand command)
    {
        var user = await _authService.Auth(command);
        return user;
    }
}