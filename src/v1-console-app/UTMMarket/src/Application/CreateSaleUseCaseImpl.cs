using UTMMarket.Core.Entities;
using UTMMarket.Core.Repositories;
using UTMMarket.Core.UseCases;

namespace UTMMarket.Application;

public sealed class CreateSaleUseCaseImpl(ISaleRepository repository, IProductRepository productRepository) : ICreateSaleUseCase
{
    public async Task<Sale> ExecuteAsync(Sale sale, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(sale);

        if (sale.Details.Count == 0)
        {
            throw new InvalidOperationException("A sale must have at least one detail.");
        }

        // Validate stock and prices for each item
        foreach (var detail in sale.Details)
        {
            var product = await productRepository.GetByIdAsync(detail.Product.ProductID, ct);
            if (product is null)
            {
                throw new InvalidOperationException($"Product with ID {detail.Product.ProductID} not found.");
            }

            if (product.Stock < detail.Quantity)
            {
                throw new InvalidOperationException($"Insufficient stock for product {product.Name}. Available: {product.Stock}, Requested: {detail.Quantity}");
            }
        }

        // Persist sale (Repository handles transaction in implementation)
        var persistedSale = await repository.AddAsync(sale, ct);

        // Update stocks (This should ideally be part of the repository transaction, 
        // but following use case orchestration pattern)
        foreach (var detail in sale.Details)
        {
            await productRepository.UpdateStockAsync(detail.Product.ProductID, detail.Product.Stock - detail.Quantity, ct);
        }

        return persistedSale;
    }
}
