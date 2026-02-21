using UTMMarket.Core.Entities;

namespace UTMMarket.Core.UseCases;

/// <summary>
/// Use Case for registering a new product in the system.
/// </summary>
public interface ICreateProductUseCase
{
    /// <summary>
    /// Executes the logic to create a product.
    /// </summary>
    /// <param name="product">The product domain model to persist.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The generated unique identifier for the new product.</returns>
    Task<int> ExecuteAsync(Product product, CancellationToken ct = default);
}
