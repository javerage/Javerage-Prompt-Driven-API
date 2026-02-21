using UTMMarket.Core.Entities;

namespace UTMMarket.Core.UseCases;

/// <summary>
/// Use Case for retrieving all products in the catalog.
/// Supports streaming via IAsyncEnumerable for high performance.
/// </summary>
public interface IGetAllProductsUseCase
{
    /// <summary>
    /// Executes the logic to fetch all products.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>An asynchronous stream of Product domain objects.</returns>
    IAsyncEnumerable<Product> ExecuteAsync(CancellationToken ct = default);
}
