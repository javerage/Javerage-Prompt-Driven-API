using UTMMarket.Core.Entities;
using UTMMarket.Core.Repositories;
using UTMMarket.Core.UseCases;

namespace UTMMarket.Application;

public sealed class GetAllProductsUseCaseImpl(IProductRepository repository) : IGetAllProductsUseCase
{
    public IAsyncEnumerable<Product> ExecuteAsync(CancellationToken ct = default)
    {
        return repository.GetAllAsync(ct);
    }
}
