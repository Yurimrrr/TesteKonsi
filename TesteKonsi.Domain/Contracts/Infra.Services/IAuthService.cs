
using TesteKonsi.Domain.ValueObjects;
using TesteKonsi.Domain.ValueObjects.Response;
using TesteKonsi.Domain.ValueObjects.Response.Auth;

namespace TesteKonsi.Domain.Contracts.Infra.Services;

public interface IAuthService
{
    Task<AuthResponse> Auth(AuthCommand request);
}