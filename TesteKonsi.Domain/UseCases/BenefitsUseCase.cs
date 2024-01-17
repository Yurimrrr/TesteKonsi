using TesteKonsi.Domain.Contracts.Infra.Services;
using TesteKonsi.Domain.Shared;
using TesteKonsi.Domain.ValueObjects;
using TesteKonsi.Domain.ValueObjects.Response;
using TesteKonsi.Domain.ValueObjects.Response.Auth;
using TesteKonsi.Domain.ValueObjects.Response.Benefits;

namespace TesteKonsi.Domain.UseCases;

public class BenefitsUseCase : IUseCase<BenefitsCommand, BenefitsResponse>
{
    private readonly IBenefitsService _benefitsService;

    public BenefitsUseCase(IBenefitsService benefitsService)
    {
        _benefitsService = benefitsService;
    }

    public async Task<BenefitsResponse> Execute(BenefitsCommand command)
    {
        var user = await _benefitsService.ConsultBenefits(command);
        return user;
    }
}