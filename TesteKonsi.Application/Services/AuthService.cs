using MediatR;
using TesteKonsi.Application.DTOs.Request;
using TesteKonsi.Application.DTOs.Response;
using TesteKonsi.Domain.Contracts.Infra.Services;
using TesteKonsi.Domain.UseCases;

namespace TesteKonsi.Application.Services;

public class AuthService : IRequestHandler<AuthRequestDTO, AuthResponseDTO>
{
    private readonly IAuthService _authService;

    public AuthService(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponseDTO> Handle(AuthRequestDTO request, CancellationToken cancellationToken)
    {
        var useCase = new AuthUseCase(_authService);

        var result = await useCase.Execute(request);
        
        return new AuthResponseDTO(result.Success, result.Data);
    }
}