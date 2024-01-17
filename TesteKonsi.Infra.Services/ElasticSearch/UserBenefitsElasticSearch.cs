using Nest;
using TesteKonsi.Application.Contracts;
using TesteKonsi.Domain.Entities;

namespace TesteKonsi.Infra.Services.ElasticSearch;

public class UserBenefitsElasticSearch : IElasticSearch
{
    private readonly IElasticClient _client;

    public UserBenefitsElasticSearch(IElasticClient client)
    {
        _client = client;
    }

    public async Task IndexingBenefit(UserBenefits userBenefits)
    {
        await _client.IndexDocumentAsync(userBenefits);
    }

    public async Task<UserBenefits> GetUserBenefits(string cpf)
    {
        var getResponse = await _client.SearchAsync<UserBenefits>(s => s
            .Query(q => q
                .Match(m => m
                    .Field(f => f.Cpf)
                    .Query(cpf)
                )
            )
        );

        if (!getResponse.IsValid)
        {
            //Tratar Erro;
        }

        return getResponse.Documents.FirstOrDefault();
    }
}