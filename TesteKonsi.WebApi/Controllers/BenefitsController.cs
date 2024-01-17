using MediatR;
using Microsoft.AspNetCore.Mvc;
using TesteKonsi.Application.Contracts;
using TesteKonsi.Application.DTOs.Request;
using TesteKonsi.Domain.Entities;
using TesteKonsi.Domain.ValueObjects;
using TesteKonsi.Domain.ValueObjects.Response.Benefits;

namespace TesteKonsi.WebApi.Controllers;

[ApiController, Route("api/v1/benefits")]
public class BenefitsController : ControllerBase
{
    private readonly IElasticSearch _elasticSearch;

    public BenefitsController(IElasticSearch elasticSearch)
    {
        _elasticSearch = elasticSearch;
    }

    [HttpGet("")]
    public async Task<UserBenefits?> GetBenefitByCpf([FromQuery] string cpf)
    {
        cpf = cpf.Replace(".", "").Replace("-", "");
        
        var result = await _elasticSearch.GetUserBenefits(cpf); 

        return result;
    }
}