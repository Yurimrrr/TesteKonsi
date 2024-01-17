
using TesteKonsi.Domain.ValueObjects;
using TesteKonsi.Domain.ValueObjects.Response.Benefits;

namespace TesteKonsi.Domain.Contracts.Infra.Services;

public interface IBenefitsService
{
    Task<BenefitsResponse> ConsultBenefits(BenefitsCommand request);
}