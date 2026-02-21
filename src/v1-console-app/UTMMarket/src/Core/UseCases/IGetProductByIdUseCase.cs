using UTMMarket.Core.Entities;

namespace UTMMarket.Core.UseCases;

/// <summary>
/// Use Case for retrieving a specific product by its unique identifier.
/// </summary>
public interface IGetProductByIdUseCase
{
    /// <summary>
    /// Executes the logic to find a product.
    /// </summary>
    /// <param name="productId">The unique ID of the product.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The Product domain object if found; otherwise, null.</returns>
    Task<Product?> ExecuteAsync(int productId, CancellationToken ct = default);
}
