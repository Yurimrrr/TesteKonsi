using TesteKonsi.Domain.ValueObjects.Response.Auth;

namespace TesteKonsi.Application.DTOs.Response;

public class AuthResponseDTO: AuthResponse
{
    public AuthResponseDTO(bool success, Data data) : base(success, data)
    {
    }
    
    public AuthResponse ConvertToDomain() => new AuthResponse(success: Success, data: Data);
}
