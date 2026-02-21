using UTMMarket.Core.Repositories;
using UTMMarket.Core.UseCases;

namespace UTMMarket.Application;

public sealed class UpdateProductStockUseCaseImpl(IProductRepository repository) : IUpdateProductStockUseCase
{
    public async Task<bool> ExecuteAsync(int productId, int newStock, CancellationToken ct = default)
    {
        if (newStock < 0) throw new ArgumentOutOfRangeException(nameof(newStock), "Stock cannot be negative.");

        var product = await repository.GetByIdAsync(productId, ct);
        if (product is null) return false;

        return await repository.UpdateStockAsync(productId, newStock, ct);
    }
}
