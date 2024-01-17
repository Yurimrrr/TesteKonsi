using MediatR;
using TesteKonsi.Application.DTOs.Response;
using TesteKonsi.Domain.ValueObjects;

namespace TesteKonsi.Application.DTOs.Request;

public class BenefitsRequestDTO: BenefitsCommand, IRequest<BenefitsConsultServiceResponseDTO>
{
    public BenefitsRequestDTO(String cpf) 
        : base(cpf)
    {
    }
}