using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using TesteKonsi.Application.Contracts.Repository.Cache;
using TesteKonsi.Application.DTOs.Request;
using TesteKonsi.Domain.Contracts.Infra.Services;
using TesteKonsi.Domain.ValueObjects;
using TesteKonsi.Domain.ValueObjects.Response.Benefits;
using TesteKonsi.Infra.Services.Configurations;
using TesteKonsi.Infra.Services.Utils;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TesteKonsi.Infra.Services.HttpServices;

public class BenefitsConsultService : IBenefitsService
{
    private readonly IHttpRequestService _httpService;
    private readonly ApiBeneficios _apiBeneficios;
    private readonly ICacheRepository _cacheRepository;
    private readonly IMediator _mediator;
    
    public BenefitsConsultService(IHttpRequestService httpService, IOptions<ApiBeneficios> apiBeneficios,
        ICacheRepository cacheRepository, IMediator mediator)
    {
        _httpService = httpService;
        _apiBeneficios = apiBeneficios.Value;
        _cacheRepository = cacheRepository;
        _mediator = mediator;
    }

    public async Task<BenefitsResponse> ConsultBenefits(BenefitsCommand request)
    {
        try
        {
            var result = await _cacheRepository.GetValue<BenefitsResponse>(request.Cpf);

            if (result is null)
            {

                if (string.IsNullOrEmpty(TokenBenefits.BearerToken))
                {
                    var authRequest = new AuthRequestDTO(_apiBeneficios.UserName, _apiBeneficios.Password);
                    var resultAuth = await _mediator.Send(authRequest);
                    if (resultAuth == null)
                    {
                        throw new Exception("Ocorreu um erro na autenticação");
                    }
                    
                    TokenBenefits.BearerToken = resultAuth.Data.Token;
                }
                
                var requestJson = JsonSerializer.Serialize(request);
        
                result = await _httpService.HttpRequest<BenefitsResponse>(
                    _apiBeneficios.Url,
                    "/api/v1/inss/consulta-beneficios?cpf="+ request.Cpf,
                    requestJson,
                    HttpMethod.Get,
                    TokenBenefits.BearerToken
                );

                var cacheOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(12));

                await _cacheRepository.SetValue<BenefitsResponse>(request.Cpf, result, cacheOptions);
            }
            
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }
}