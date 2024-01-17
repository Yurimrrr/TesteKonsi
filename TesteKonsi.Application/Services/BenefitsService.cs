using MediatR;
using TesteKonsi.Application.DTOs.Request;
using TesteKonsi.Application.DTOs.Response;
using TesteKonsi.Domain.Contracts.Infra.Services;
using TesteKonsi.Domain.UseCases;

namespace TesteKonsi.Application.Services;

public class BenefitsService : IRequestHandler<BenefitsRequestDTO, BenefitsConsultServiceResponseDTO>
{
    private readonly IBenefitsService _benefitsService;

    public BenefitsService(IBenefitsService benefitsService)
    {
        _benefitsService = benefitsService;
    }

    public async Task<BenefitsConsultServiceResponseDTO> Handle(BenefitsRequestDTO request, CancellationToken cancellationToken)
    {
        var useCase = new BenefitsUseCase(_benefitsService);

        var result = await useCase.Execute(request);
        
        return new BenefitsConsultServiceResponseDTO(result.Success, result.Data);
    }
}