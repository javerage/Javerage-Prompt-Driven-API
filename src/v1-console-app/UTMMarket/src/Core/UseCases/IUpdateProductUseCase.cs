using UTMMarket.Core.Entities;

namespace UTMMarket.Core.UseCases;

/// <summary>
/// Use Case for updating the information of an existing product.
/// </summary>
public interface IUpdateProductUseCase
{
    /// <summary>
    /// Executes the logic to update a product's data.
    /// </summary>
    /// <param name="product">The product domain model with updated information.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>True if the update was successful; otherwise, false.</returns>
    Task<bool> ExecuteAsync(Product product, CancellationToken ct = default);
}
