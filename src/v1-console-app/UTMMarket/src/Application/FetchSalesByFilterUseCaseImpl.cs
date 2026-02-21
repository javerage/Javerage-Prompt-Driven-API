using UTMMarket.Core.Entities;
using UTMMarket.Core.Repositories;
using UTMMarket.Core.UseCases;

namespace UTMMarket.Application;

public sealed class FetchSalesByFilterUseCaseImpl(ISaleRepository repository) : IFetchSalesByFilterUseCase
{
    public IAsyncEnumerable<Sale> ExecuteAsync(SaleFilter filter, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(filter);
        return repository.FindAsync(filter, ct);
    }
}
