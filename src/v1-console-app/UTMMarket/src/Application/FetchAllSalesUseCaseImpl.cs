using UTMMarket.Core.Entities;
using UTMMarket.Core.Repositories;
using UTMMarket.Core.UseCases;

namespace UTMMarket.Application;

public sealed class FetchAllSalesUseCaseImpl(ISaleRepository repository) : IFetchAllSalesUseCase
{
    public IAsyncEnumerable<Sale> ExecuteAsync(CancellationToken ct = default)
    {
        return repository.GetAllAsync(ct);
    }
}
