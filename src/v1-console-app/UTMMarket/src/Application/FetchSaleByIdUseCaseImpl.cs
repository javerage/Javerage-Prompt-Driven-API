using UTMMarket.Core.Entities;
using UTMMarket.Core.Repositories;
using UTMMarket.Core.UseCases;

namespace UTMMarket.Application;

public sealed class FetchSaleByIdUseCaseImpl(ISaleRepository repository) : IFetchSaleByIdUseCase
{
    public Task<Sale?> ExecuteAsync(int id, CancellationToken ct = default)
    {
        return repository.GetByIdAsync(id, ct);
    }
}
