using UTMMarket.Core.Entities;
using UTMMarket.Core.Repositories;
using UTMMarket.Core.UseCases;

namespace UTMMarket.Application;

public sealed class CreateProductUseCaseImpl(IProductRepository repository) : ICreateProductUseCase
{
    public async Task<int> ExecuteAsync(Product product, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(product);
        
        // Business Rule: SKU must be unique
        Product? existing = null;
        await foreach (var p in repository.FindAsync(new ProductFilter(SKU: product.SKU), ct))
        {
            existing = p;
            break;
        }

        if (existing is not null)
        {
            throw new InvalidOperationException($"Product with SKU {product.SKU} already exists.");
        }

        return await repository.AddAsync(product, ct);
    }
}
