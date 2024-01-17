using TesteKonsi.Domain.ValueObjects.Response.Benefits;

namespace TesteKonsi.Application.DTOs.Response;

public class BenefitsConsultServiceResponseDTO : BenefitsResponse
{
    public BenefitsConsultServiceResponseDTO(bool success, Data data) 
        : base(success, data)
    {
    }
}