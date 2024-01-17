using MediatR;
using TesteKonsi.Application.DTOs.Response;
using TesteKonsi.Domain.ValueObjects;

namespace TesteKonsi.Application.DTOs.Request;

public class AuthRequestDTO: AuthCommand, IRequest<AuthResponseDTO>
{
    public AuthRequestDTO(String username, String password) 
        : base(username, password)
    {
    }

    public AuthCommand ConvertToDomain() => new AuthCommand(username: Username, password: Password);
}