using UTMMarket.Core.Entities;
using UTMMarket.Core.Repositories;
using UTMMarket.Core.UseCases;

namespace UTMMarket.Application;

public sealed class GetProductByIdUseCaseImpl(IProductRepository repository) : IGetProductByIdUseCase
{
    public Task<Product?> ExecuteAsync(int productId, CancellationToken ct = default)
    {
        return repository.GetByIdAsync(productId, ct);
    }
}
