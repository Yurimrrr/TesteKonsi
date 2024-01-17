using TesteKonsi.Domain.ValueObjects;

namespace TesteKonsi.Domain.Shared;

public interface IUseCase<T, TResult> where T : CommandBase
{
    public Task<TResult> Execute(T command);
}