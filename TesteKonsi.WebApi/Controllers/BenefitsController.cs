using MediatR;
using Microsoft.AspNetCore.Mvc;
using TesteKonsi.Application.DTOs.Request;
using TesteKonsi.Domain.ValueObjects;
using TesteKonsi.Domain.ValueObjects.Response.Benefits;

namespace TesteKonsi.WebApi.Controllers;

[ApiController, Route("api/v1/benefits")]
public class BenefitsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BenefitsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("")]
    public async Task<BenefitsResponse?> GetBenefitByCpf([FromQuery] string cpf)
    {
        var command = new BenefitsRequestDTO(cpf);
        var result = await _mediator.Send(command);

        return result;
    }
}