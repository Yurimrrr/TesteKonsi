using Moq;
using TesteKonsi.Application.DTOs.Request;
using TesteKonsi.Application.DTOs.Response;
using TesteKonsi.Domain.Contracts.Infra.Services;
using TesteKonsi.Domain.UseCases;

namespace TesteKonsi.Tests.Application;

public class AuthServiceTest
{
    private readonly Mock<IAuthService> _authService;

    public AuthServiceTest()
    {
        _authService = new();
    }
    [Fact]
    public async void AuthReturnSuccess()
    {
        var request = new AuthRequestDTO("test@konsi.com.br", "Test@Konsi2023*");
        
        var useCase = new AuthUseCase(_authService.Object);

        var result = await useCase.Execute(request);
        
        Assert.True(result.Success);
    }
}