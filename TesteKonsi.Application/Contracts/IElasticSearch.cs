using TesteKonsi.Domain.Entities;

namespace TesteKonsi.Application.Contracts;

public interface IElasticSearch
{
    Task IndexingBenefit(UserBenefits userBenefits);
    Task<UserBenefits> GetUserBenefits(string cpf);
}