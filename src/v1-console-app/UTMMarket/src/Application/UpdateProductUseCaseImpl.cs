using UTMMarket.Core.Entities;
using UTMMarket.Core.Repositories;
using UTMMarket.Core.UseCases;

namespace UTMMarket.Application;

public sealed class UpdateProductUseCaseImpl(IProductRepository repository) : IUpdateProductUseCase
{
    public async Task<bool> ExecuteAsync(Product product, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(product);

        var existing = await repository.GetByIdAsync(product.ProductID, ct);
        if (existing is null) return false;

        return await repository.UpdateAsync(product, ct);
    }
}
